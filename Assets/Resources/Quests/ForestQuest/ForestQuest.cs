﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestQuest : Quest
{
    private string NpcId = "GrandMa";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(data.description);
        RunTasksQueue(0);
        InitQuest();
    }

    void InitQuest()
    {

        EventListener.instance.Interaction.AddListener(data =>
        {
            if (data.NpcId == NpcId)
            {
                /*data.DialogueParser.Parse(data.NpcId).AddListener(() =>
                {
                    data.EndInteraction();
                    Debug.Log("Koniec Dialogu");
                });*/
            }
        });
    }

    private void RunTasksQueue(int i)
    {
        if (data.tasks.Count > i)
        {
            data.tasks[i].Run();
            data.tasks[i].onFinish.AddListener(() =>
            {
                RunTasksQueue(i + 1);
                Debug.Log("Task finished");
            });
        }
        else
        {
            //Finish Quest
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PlayerAboartQuest()
    {
        Debug.Log("Quest aboard");
    }

    public override void PlayerAcceptQuest()
    {
        QuestManager.instance.Quests["ForestQuest"].isActive = true;
    }

    private void Awake()
    {
        InitNPC("GrandMa");
        dialoguesQueue["GrandMa"].Enqueue("init");
    }

    public void init()
    {
        Debug.Log("Wszyszto dziala");
    }
}
