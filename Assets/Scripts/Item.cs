﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {Coins, Gun, Ammo, Phone, Tools};

public class Item
{
    public Sprite sprite;
    public string itemId;
    public int defaultNumber;
    public int minNumber;
    public int maxNumber;
    public int number;
    public int cost = 0;
    public GameObject model;
    public ItemType type;
    public System.Type type2 = typeof(Item);

    public Item() { }

    public Item(string spriteName)
    {
        LoadSprite(spriteName);
    } 
    
    public Item(Item item)
    {
        itemId = item.itemId;
        defaultNumber = item.defaultNumber;
        minNumber = item.minNumber;
        maxNumber = item.maxNumber;
        number = item.number;
        type = item.type;
        model = item.model;
        sprite = item.sprite;
    }
    
    public void LoadSprite(string spriteName) {
        this.sprite = Resources.Load<Sprite>("Sprites/Items/" + spriteName);
    }

    public virtual Item CreateInstance()
    {
        return new Item();
    }
}
