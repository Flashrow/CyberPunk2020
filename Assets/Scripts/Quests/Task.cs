using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum TaskType { KILL, BUY, SELL, TAKE, FIND ,TALK, GO };
[System.Serializable]
public class Task : ScriptableObject
{
    public string id;
    public string QuestId;
    public string title;
    public string description;
    public QuestStatus status;
    public int order;
    public TaskType type;
    public UnityEvent onFinish;

    protected virtual void OnEnable()
    {
        if (onFinish == null) onFinish = new UnityEvent();
    }


    public virtual void Run()
    {

    }

    public virtual void Finish()
    {

    }
}
