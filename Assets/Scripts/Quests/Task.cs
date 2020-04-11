using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TaskType { KILL, BUY, SELL, TAKE, FIND ,TALK, GO };
public class Task : MonoBehaviour
{
    public string id;
    public string title;
    public string description;
    public QuestStatus status;
    public int order;
    public TaskType type;
}
