using System.Collections;
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

    public override void RemoveQuest()
    {
        // TODO: add remove component from unity (NPC)
        QuestManager.instance.Quests.Remove("ForestQuest");
    }

    public override void ChangeQuestToTodo()
    {
        QuestManager.instance.Quests["ForestQuest"].data.status = QuestStatus.TODO;
    }

    public override void ChangeQuestToInProgress()
    {
        QuestManager.instance.Quests["ForestQuest"].data.status = QuestStatus.IN_PROGRESS;
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
