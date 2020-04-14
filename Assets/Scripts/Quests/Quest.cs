using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum QuestStatus { IN_PROGRESS, DONE, TODO};
public abstract class Quest : MonoBehaviour
{
    public string QuestId;
    public QuestData data;
    public Dictionary<string, Task> tasks = new Dictionary<string, Task>();
    public Task activeTask;

    public void UnmountQuest()
    {
        Destroy(this);
    }

    public void LoadQuestData(string dataFile)
    {
        data = Resources.Load<QuestData>($"Quests/{dataFile}");
    }

    public void LoadQuestData(QuestData dataFile)
    {
        data = dataFile;
    }
}
