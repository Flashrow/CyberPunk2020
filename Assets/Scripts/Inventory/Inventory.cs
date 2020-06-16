using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu (fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
public class Inventory : ScriptableObject {

    public Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item> ();
    public Dictionary<Slots, Item> slots = new Dictionary<Slots, Item> ();

    public delegate void InventoryChanged ();
    public static InventoryChanged onInventoryChange;

    public delegate void AddedItemInventory (Item item);
    public static AddedItemInventory onAddItemInventory;

    public FlyingItem FlyingItemPreFab;

    public void AddItem(Item item)
    {
        if (!items.ContainsKey(item.data.type))
        {
            items.Add(item.data.type, item);
            try
            {
                onAddItemInventory(item);
            }
            catch 
            {
                Debug.Log("Inventory: onAddItemInventory()");
            }
        }
        else
        {
            items[item.data.type].number += item.number;
        }
    }

    public void IncreaseItem (ItemType type, int value) {
        items[type].number += value;
        onInventoryChange ();
    }

    public void DecreaseItem (ItemType type, int value) {
        items[type].number -= value;
        if (items[type].number <= 0) items.Remove (type);
        onInventoryChange ();
    }

    public void RemoveItem (ItemType type) {
        items.Remove (type);
    }

    public void MoveItemToCharacter (Item item) {
        switch (item.data.slot) {
            case Slots.Secondary:
                if (slots.ContainsKey (Slots.Secondary))
                    MoveItemToInventory (Slots.Secondary);
                slots.Add (Slots.Secondary, items[item.data.type].CreateInstance ());
                items[item.data.type].number -= 1;
                slots[Slots.Secondary].number = 1;
                break;
            case Slots.Primary:
                if (slots.ContainsKey (Slots.Primary))
                    MoveItemToInventory (Slots.Primary);
                slots.Add (Slots.Primary, items[item.data.type].CreateInstance ());
                items[item.data.type].number -= 1;
                slots[Slots.Primary].number = 1;
                break;
        }
        if (items[item.data.type].number <= 0) items.Remove (item.data.type);
        onInventoryChange ();
        WeaponManager.instance.updateWeapon();
    }

    public void MoveItemToInventory (Slots slot) {
        if (!slots.ContainsKey (slot)) return;
        AddItem (slots[slot]);
        slots.Remove (slot);
        try {
            onInventoryChange ();
        } catch { };
    }

    void DisplayItemInTheWorld (Item item) {
        Vector3 pos = GameObject.FindObjectOfType<Hero> ().transform.position;
        pos += Vector3.forward * 2;
        FlyingItem display = (FlyingItem) Instantiate (FlyingItemPreFab);
        display.Prime (item);
        display.transform.position = pos;
    }

    public void DropItem (ItemType type) {
        DisplayItemInTheWorld (items[type]);
        items.Remove (type);
        try {
            onInventoryChange ();
        } catch { };
    }

    public void DropItem (Slots slot) {
        DisplayItemInTheWorld (slots[slot]);
        slots.Remove (slot);
        try {
            onInventoryChange ();
        } catch { };
    }

    public bool IsSlotEmpty (Slots slot) {
        return !slots.ContainsKey (slot);
    }

    public bool HasItem (ItemType type) {
        return items.ContainsKey (type);
    }

    public void forceUpdate()
    {
        try
        {
            onInventoryChange();
        }
        catch { };
    }
}