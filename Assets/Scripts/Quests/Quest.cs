using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum QuestStatus { IN_PROGRESS, DONE, TODO};
public abstract class Quest : MonoBehaviour
{
    public string QuestId;
    public string title;
    public string description;
    public QuestStatus status;
    public int order;
    public Dictionary<string, Task> tasks = new Dictionary<string, Task>();
    public Task activeTask;
}
