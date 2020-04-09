using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();
    public Quest activeQuest;
}
