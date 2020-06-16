using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Coins, Rifle, Ammo, Phone, Tools, Onion }

[System.Serializable]
public class Item: ISerializable {
    [NonSerialized] public ItemScriptable data;
    [SerializeField] public string itemId;
    [SerializeField] public int number;
    [SerializeField] public int cost = 0;
    [SerializeField] public string name = "";
    //public GameObject model;
    //public ItemType type;
    //public int defaultNumber;
    //public int minNumber;
    //public int maxNumber;
    //public Sprite sprite;

    public Item(SerializationInfo info, StreamingContext context)
    {
        itemId = (string)info.GetValue("itemId", typeof(string));
        number = (int)info.GetValue("number", typeof(int));
        cost = (int)info.GetValue("cost", typeof(int));
        name = (string)info.GetValue("name", typeof(string));
        LoadScriptableObject(name);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("itemId", itemId);
        info.AddValue("number", number);
        info.AddValue("cost", cost);
        info.AddValue("name", name);
    }

    public Item () { }

    public Item (string spriteName, string scriptName) {
        //LoadSprite(spriteName);
        this.name = scriptName;
        LoadScriptableObject (scriptName);
    }

    public Item (Item item) {
        data = item.data;

        itemId = item.itemId;
        //defaultNumber = item.defaultNumber;
        //minNumber = item.minNumber;
        //maxNumber = item.maxNumber;
        number = item.number;
        cost = item.cost;
        //type = item.type;
        //model = item.model;
        //sprite = item.sprite;
        name = item.name;    }

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