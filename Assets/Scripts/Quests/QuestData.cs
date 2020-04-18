using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestInfo", order = 1)]
public class QuestData : ScriptableObject
{
    public string QuestId;
    public string title;
    public string description;
    public QuestStatus status;
    public string QuestClassString;
    public Type QuestClass;
    public List<Task> tasks = new List<Task>();

    private void OnEnable()
    {
        QuestClass = Type.GetType(QuestClassString);
    }
}