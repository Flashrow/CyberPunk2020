using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    public Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item>();
    public Dictionary<Slots, Item> slots = new Dictionary<Slots, Item>();

    public delegate void InventoryChanged();
    public static InventoryChanged onInventoryChange;

    public delegate void AddedItemInventory(Item item);
    public static AddedItemInventory onAddItemInventory;

    public void AddItem(Item item)
    {
        try
        {
            items.Add(item.data.type, item);
            try
            {
                onAddItemInventory(item);
            }
            catch { }
        } catch
        {
            items[item.data.type].number += item.number;
        }
    }

    public void IncreaseItem(ItemType type, int value)
    {
        items[type].number += value;
    }

    public void DecreaseItem(ItemType type, int value)
    {
        items[type].number -= value;
        if (items[type].number <= 0) items.Remove(type);
    }

    public void RemoveItem(ItemType type)
    {
        items.Remove(type);
    }

    public void MoveItemToCharacter(Item item)
    {
        switch (item.data.slot)
        {
            case Slots.LeftHand:
                if(slots.ContainsKey(Slots.LeftHand))
                    MoveItemToInventory(Slots.LeftHand);
                slots.Add(Slots.LeftHand, items[item.data.type].CreateInstance());
                items[item.data.type].number -= 1;
                slots[Slots.LeftHand].number = 1;
                break;
            case Slots.RightHand:
                if (slots.ContainsKey(Slots.RightHand))
                    MoveItemToInventory(Slots.RightHand);
                slots.Add(Slots.RightHand, items[item.data.type].CreateInstance());
                items[item.data.type].number -= 1;
                slots[Slots.RightHand].number = 1;
                break;
        }
        if (items[item.data.type].number <= 0) items.Remove(item.data.type);
        onInventoryChange();
    }

    public void MoveItemToInventory(Slots slot)
    {
        if (!slots.ContainsKey(slot)) return;
        AddItem(slots[slot]);
        slots.Remove(slot);
        try
        {
            onInventoryChange();
        }
        catch { };
    }

    public void DropItem(ItemType type)
    {
        items.Remove(type);
        try
        {
            onInventoryChange();
        }
        catch { };
    }

    public void DropItem(Slots slot)
    {
        slots.Remove(slot);
        try
        {
            onInventoryChange();
        }
        catch { };
    }

    public bool IsSlotEmpty(Slots slot)
    {
        return !slots.ContainsKey(slot);
    }

    public bool HasItem(ItemType type)
    {
        return items.ContainsKey(type);
    }
}
