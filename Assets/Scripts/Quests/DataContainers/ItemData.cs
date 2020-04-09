using UnityEngine;

public enum ItemEventType { BUYED, SOLD, DROPPED, TAKEN };

public class ItemData
{
    public Item item;
    public ItemEventType eventType;
    public Vector3 position;
    public string NpcId;
}
