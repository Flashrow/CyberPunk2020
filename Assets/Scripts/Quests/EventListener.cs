using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public class InventoryEvent : UnityEvent<ItemData> { };
    public class KillsEvent : UnityEvent<KillData> { };
    public class DialoguesEvent : UnityEvent { };
    public class PathEvent : UnityEvent<PathElementData> { };
    public class InteractionEvent : UnityEvent<InteractionData> { };

    public static EventListener instance;

    public InventoryEvent Inventory;
    public KillsEvent Kills;
    public DialoguesEvent Dialogues;
    public PathEvent Path;
    public InteractionEvent Interaction;

    void Awake()
    {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;

        if (Path == null)
            Path = new PathEvent();
        if (Dialogues == null)
            Dialogues= new DialoguesEvent();
        if (Kills == null)
            Kills = new KillsEvent();
        if (Inventory == null)
            Inventory = new InventoryEvent();
        if (Interaction == null)
            Interaction = new InteractionEvent();
        DontDestroyOnLoad(this);
    }
}
