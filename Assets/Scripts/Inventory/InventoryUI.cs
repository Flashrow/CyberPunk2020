using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public Transform characterItems;
    public Transform items;
    public Transform weapons;

    public InventoryItem itemPreFab;
    public InventoryItem itemCharacterPreFab;

    [SerializeField]
    private Inventory inventory;
    void Start () { }

    // Update is called once per frame
    void Update () {

    }
    void DisplayItem (Item item) {
        InventoryItem display = (InventoryItem) Instantiate (itemPreFab);
        if (item is Ammo) {
            display.transform.SetParent (weapons);
        } else {
            display.transform.SetParent (items);
        }
        display.transform.localScale = new Vector3 (1, 1, 1);
        display.Prime (item);
    }

    private void OnEnable () {
        Inventory.onAddItemInventory += DisplayItem;
        foreach (KeyValuePair<ItemType, Item> item in inventory.items) {
            DisplayItem (item.Value);
        }
    }

    private void OnDisable () {
        Inventory.onAddItemInventory -= DisplayItem;
        foreach (Transform child in items) {
            GameObject.Destroy (child.gameObject);
        }
        foreach (Transform child in weapons) {
            GameObject.Destroy (child.gameObject);
        }
    }
}