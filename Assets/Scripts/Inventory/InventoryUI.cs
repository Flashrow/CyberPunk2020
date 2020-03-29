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
    // Use this for initialization
 
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
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
    }

    private void OnDisable()
    {
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
