using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestInfo", order = 1), System.Serializable]
public class QuestData : ScriptableObject
{
    public string QuestId;
    public string title;
    public string description;
    public QuestStatus status = QuestStatus.EXCLUDED;
    public string QuestClassString;
    public Type QuestClass;
    public List<Task> tasks = new List<Task>();

    private void OnEnable()
    {
        QuestClass = Type.GetType(QuestClassString);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(QuestData), true, isFallback = true)]
public class myClassEditor : Editor
{
    QuestData _target;

    private ReorderableList _myList;

    public void OnEnable()
    {

        _target = (QuestData)target;

        _myList = new ReorderableList(serializedObject, serializedObject.FindProperty("tasks"), true, true, true, true);

        _myList.drawHeaderCallback = rect => {
            EditorGUI.LabelField(rect, "Tasks", EditorStyles.boldLabel);
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