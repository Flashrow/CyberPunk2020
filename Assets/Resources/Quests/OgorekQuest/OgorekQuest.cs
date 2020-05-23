using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgorekQuest : Quest
{
    private string NpcId = "unitychan";
    private void Awake()
    {
        InitNPC("unitychan");
        dialoguesQueue["unitychan"].Enqueue("init");
    }
    void Start()
    {
        Debug.Log(data.description);
        InitQuest();
    }
    void InitQuest()
    {
        EventListener.instance.Interaction.AddListener(data =>
        {
            if (data.NpcId == NpcId)
            {

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
            QuestManager.instance.Quests["OgorekQuest"].data.status = QuestStatus.DONE;
        }
    }

    public void RunAction()
    {
        RunTasksQueue(0);
        ChangeQuestToInProgress();
    }

    public override void AboardQuest()
    {
        QuestManager.instance.Quests["OgorekQuest"].data.status = QuestStatus.EXCLUDED;
    }
    public override void ChangeQuestToTodo()
    {
        QuestManager.instance.Quests["OgorekQuest"].data.status = QuestStatus.TODO;
    }
    public override void ChangeQuestToInProgress()
    {
        QuestManager.instance.Quests["OgorekQuest"].data.status = QuestStatus.IN_PROGRESS;
    }
}
