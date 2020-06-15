using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class FlyingItem : Interacted, ISerializable
{
    [SerializeField]
    Item item;
    
    [SerializeField]
    private SpriteRenderer sprite;

    public FlyingItem(SerializationInfo info, StreamingContext context)
    {
        item = (Item)info.GetValue("item", typeof(Item));
        sprite = (SpriteRenderer)info.GetValue("sprite", typeof(SpriteRenderer));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("item", item);
        info.AddValue("sprite", sprite);

    }

    public override void OnInteract () {
        PlayerManager.Instance.HeroScript.inventory.AddItem (item);
        EventListener.instance.Inventory.Invoke(new ItemData
        {
            item = item,
            eventType = ItemEventType.TAKEN
        });
        Destroy (gameObject);
    }

    public void Prime (Item item) {
        sprite.sprite = item.data.sprite;
        this.item = item;
    }
}