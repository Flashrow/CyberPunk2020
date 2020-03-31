using UnityEngine;
using System.Collections;

public class FlyingItem : MonoBehaviour
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

    public void Interact()
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
