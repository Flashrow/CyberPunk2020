using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    public Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item>();

    public delegate void InventoryChanged();
    public static InventoryChanged onInventoryChange;

    public Item item1;
    public Item item2;

    public void AddItem(Item item)
    {
        try
        {
            items.Add(item.type, item);
        } catch
        {
            items[item.type].number += item.number;
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
        items[item.type].number -= 1;
        item1 = items[item.type].CreateInstance();
        item1.number = 1;
        Debug.Log(item1.itemId);
        if (items[item.type].number <= 0) items.Remove(item.type);
        onInventoryChange();
    }

    public void MoveItemToInventory(Item item)
    {

    }
}
