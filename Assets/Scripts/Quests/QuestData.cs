using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestInfo", order = 1)]
public class QuestData : ScriptableObject
{
    public string QuestId;
    public string title;
    public string description;
    public QuestStatus status;
    public string QuestClassString;
    public Type QuestClass;

    private void OnEnable()
    {
        QuestClass = Type.GetType(QuestClassString);
    }
}