using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditorInternal;
using UnityEditor;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public List<QuestData> Quests = new List<QuestData>();
    public Quest activeQuest;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one QuestManager in the scene");
        }
        else
        {
            instance = this;
        }
        MountQuest(Quests[0]);
    }

    private void Update()
    {
        
    }

    public void MountQuest(QuestData questData)
    {
        gameObject.AddComponent(questData.QuestClass);
        activeQuest = gameObject.GetComponent(questData.QuestClass) as Quest;
        activeQuest.LoadQuestData(questData);
    }
}


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
