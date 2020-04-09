using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventListener
{
    public static UnityEvent<ItemData> Inventory;
    public static UnityEvent<KillData> Kills;
    public static UnityEvent Dialogues;
}
