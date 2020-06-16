using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
#endif

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();

    private void Awake()
    {
       instance = this;
    }

    public void MountQuest(QuestData questData)
    {
        if(Quests.ContainsKey(questData.QuestId) == false)
        {
            gameObject.AddComponent(questData.QuestClass);
            var obj = gameObject.GetComponent(questData.QuestClass) as Quest;
            obj.LoadQuestData(questData);
            Quests.Add(questData.QuestId, obj);
        } else
        {
            if (Quests[questData.QuestId].data.status == QuestStatus.EXCLUDED)
            {
                //Debug.LogWarning($"Quest { questData.tasks} was aboard by player earlier");
                UImanager.Alert($"Quest: {questData.name} is inaccessible");
            }
            else
            {
                UImanager.Alert($"Quest: {questData.name} in progress");
                //Debug.Log($"Quest {questData.tasks} in progress...");
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(QuestManager), true, isFallback = true)]
public class QuestManagerEditor : Editor
{
    QuestManager _target;

    private ReorderableList _myList;

    public void OnEnable()
    {

        _target = (QuestManager)target;

        _myList = new ReorderableList(serializedObject, serializedObject.FindProperty("Quests"), true, true, true, true);

        _myList.drawHeaderCallback = rect => {
            EditorGUI.LabelField(rect, "Quests", EditorStyles.boldLabel);
        };

        _myList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = _myList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        _myList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

}
#endif