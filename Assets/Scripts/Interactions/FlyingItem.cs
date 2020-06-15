using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class FlyingItem : Interacted
{
    [SerializeField]
    Item item;
    
    private SpriteRenderer sprite;

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