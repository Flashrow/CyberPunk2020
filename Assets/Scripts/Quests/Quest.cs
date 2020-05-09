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
    TargetsDataMinimap tg;

    void Awake()
    {
        tg = Resources.Load<TargetsDataMinimap>("TargetsDataMinimap");
    }

    public void UnmountQuest()
    {
        Destroy(this);
    }

    public void LoadQuestData(QuestData dataFile)
    {
        data = dataFile;
        // TODO: implement TargetsDataMinimap - only if player should see target all time
    }

    public void InitNPC(string NPCid)
    {
        dialoguesQueue.Add(NPCid, new Queue<string>());
    }

    public abstract void AboardQuest();
    public virtual  void RemoveQuest()
    {
        QuestManager.instance.Quests.Remove(QuestId);
    }
    public abstract void ChangeQuestToTodo();
    public abstract void ChangeQuestToInProgress();
}

