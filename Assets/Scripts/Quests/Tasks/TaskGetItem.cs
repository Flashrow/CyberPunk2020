using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TaskGetItem", menuName = "Tasks/Get Item", order = 2), System.Serializable]
public class TaskGetItem : Task
{
    public ItemScriptable item;
    public ItemEventType itemEventType;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Run()
    {
        status = QuestStatus.IN_PROGRESS;
        EventListener.instance.Inventory.AddListener(OnItemAdded);
    }

    private void OnItemAdded(ItemData itemData)
    {
        if (itemData.item.data.type == item.type 
            && itemEventType == itemData.eventType) 
            Finish();
    }

    public override void Finish()
    {
        status = QuestStatus.DONE;
        EventListener.instance.Inventory.RemoveListener(OnItemAdded);
        base.Finish();
    }
}