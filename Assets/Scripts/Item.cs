using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Coins, Rifle, Ammo, Phone, Tools, Onion }

public class Item {
    public string itemId;
    public int number;
    public int cost = 0;
    public ItemScriptable data;
    //public GameObject model;
    //public ItemType type;
    //public int defaultNumber;
    //public int minNumber;
    //public int maxNumber;
    //public Sprite sprite;

    public Item () { }

    public Item (string spriteName, string scriptName) {
        //LoadSprite(spriteName);
        LoadScriptableObject (scriptName);
    }

    public Item (Item item) {
        itemId = item.itemId;
        //defaultNumber = item.defaultNumber;
        //minNumber = item.minNumber;
        //maxNumber = item.maxNumber;
        number = item.number;
        cost = item.cost;
        //type = item.type;
        //model = item.model;
        //sprite = item.sprite;
        data = item.data;
    }

    public void LoadSprite (string spriteName) {
        //this.sprite = Resources.Load<Sprite>("Sprites/Items/" + spriteName);
    }

    public virtual void LoadScriptableObject (string scriptName) {
        this.data = Resources.Load<ItemScriptable> ("Items/" + scriptName);
    }

    public virtual Item CreateInstance () {
        return new Item(this);
    }

    static public Item CreateItemObjectByType (ItemType type) {
        switch (type) {
            case ItemType.Ammo:
                return new Ammo ();
            case ItemType.Coins:
                return new Coins ();
            case ItemType.Phone:
                return new Phone ();
            case ItemType.Tools:
                return new Tools();
            case ItemType.Rifle:
                return new Weapon("rifle","rifle");
            case ItemType.Onion:
                return new Item("cebula","Onion");
            default:
                return new Item ();
        }
    }

}