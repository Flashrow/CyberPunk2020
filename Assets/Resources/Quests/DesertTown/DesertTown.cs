using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DesertTown : Quest
{

    private const string BOSS = "Boss";
    private void Awake()
    {
        InitNPC(BOSS);
        dialoguesQueue[BOSS].Enqueue("init");
        data.tasks[0].onFinish.AddListener(EnqueueEndDialogue);
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
            QuestManager.instance.Quests["DesertTown"].data.status = QuestStatus.DONE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void accept()
    {
        ChangeQuestToInProgress();
        RunTasksQueue(0);
        dialoguesQueue[BOSS].Enqueue("inProgress");
    }

    public void award()
    {
        Resources.Load<MoneySystem>("MoneyData").Add(1000);
    }

    public void EnqueueEndDialogue()
    {
        dialoguesQueue[BOSS].Enqueue("end");
    }

    public override void AboardQuest()
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeQuestToInProgress()
    {
        QuestManager.instance.Quests["DesertTown"].data.status = QuestStatus.IN_PROGRESS;
    }

    public override void ChangeQuestToTodo()
    {
        throw new System.NotImplementedException();
    }
}
