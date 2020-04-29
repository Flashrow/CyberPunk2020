using System.Collections;
using UnityEngine;

public class FlyingItem : Interacted {
    Item item;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Inventory inventory;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public override void OnInteract () {
        inventory.AddItem (item);
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