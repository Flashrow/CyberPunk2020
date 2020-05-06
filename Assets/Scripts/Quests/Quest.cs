using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum QuestStatus { IN_PROGRESS, DONE, TODO, EXCLUDED};
public abstract class Quest : MonoBehaviour
{
    public string QuestId;
    public QuestData data;
    public Task activeTask;
    public Dictionary<string, Queue<string>> dialoguesQueue = new Dictionary<string, Queue<string>>();

    public void UnmountQuest()
    {
        Destroy(this);
    }

    public void LoadQuestData(QuestData dataFile)
    {
        data = dataFile;
    }

    public void InitNPC(string NPCid)
    {
        dialoguesQueue.Add(NPCid, new Queue<string>());
    }

    public abstract void RemoveQuest();
    public abstract void ChangeQuestToTodo();
    public abstract void ChangeQuestToInProgress();
}

