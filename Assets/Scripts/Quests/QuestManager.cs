using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
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
        Quest quest = gameObject.GetComponent(questData.QuestClass) as Quest;
        quest.LoadQuestData(questData);
    }
}
