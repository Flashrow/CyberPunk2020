using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    // Start is called before the first frame update
    public Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item>();

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
}
