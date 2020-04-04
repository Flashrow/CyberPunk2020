using UnityEngine;
using System.Collections;

public class FlyingItem : Interacted
{
    Item item;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Inventory inventory;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnInteract()
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }

    public void Prime(Item item)
    {
        sprite.sprite = item.data.sprite;
        this.item = item;
    }
}
