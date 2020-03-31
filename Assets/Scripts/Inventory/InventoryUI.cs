using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InventoryUI : MonoBehaviour
{
    public Transform characterItems;
    public Transform items;
    public Transform weapons;

    public InventoryItem itemPreFab;
    public InventoryItem itemCharacterPreFab;

    [SerializeField]
    private Inventory inventory;

    //Character items fields
    [SerializeField]
    private InventoryItem item1;
    [SerializeField]
    private InventoryItem item2;

    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void refresh()
    {
        OnDisable();
        OnEnable();
    }

    private void OnEnable()
    {
        Inventory.onInventoryChange += refresh;
        foreach (KeyValuePair<ItemType, Item> item in inventory.items)
        {
            InventoryItem display = (InventoryItem)Instantiate(itemPreFab);
            if(item.Value is Ammo)
            {
                display.transform.SetParent(weapons);
            } else
            {
                display.transform.SetParent(items);
            }
            display.transform.localScale = new Vector3(1, 1, 1);
            display.Prime(item.Value);
        }
        if(inventory.item1 != null)
        {
            item1.Prime(inventory.item1);
        }
    }

    private void OnDisable()
    {
        Inventory.onInventoryChange -= refresh;
        foreach (Transform child in items)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in weapons)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
