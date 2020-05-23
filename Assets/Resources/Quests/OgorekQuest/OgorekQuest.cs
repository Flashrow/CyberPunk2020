using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        if (data.tasks.Count > i &&
            data.tasks[i].status == QuestStatus.TODO)
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
        data.tasks.ForEach(task =>
        {
            if (task.id.Contains("getPart"))
            {
                task.Run();
                task.onFinish.AddListener(()=>
                {
                    if (checkIfGetPartsDone())
                    {
                        dialoguesQueue["unitychan"].Clear();
                        dialoguesQueue["unitychan"].Enqueue("unitychan_finish"); 
                    }
                });
            }
        });

        RunTasksQueue(0);
        ChangeQuestToInProgress();
        dialoguesQueue["unitychan"].Enqueue("unitychan_duringGetPartQuest");
    }

    private bool checkIfGetPartsDone()
    {
        foreach(var task in data.tasks)
        {
            if (task.id.Contains("getPart") &&
                task.status != QuestStatus.DONE)
            {
               return false;
            }
        }
      return true;
    }

    public void FinishQuest()
    {
        dialoguesQueue["unitychan"].Enqueue("unitychan_history");
        QuestManager.instance.Quests["OgorekQuest"].data.status = QuestStatus.DONE;
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
