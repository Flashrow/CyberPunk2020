using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Coins, Gun, Ammo, Phone, Tools }

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
        //type = item.type;
        //model = item.model;
        //sprite = item.sprite;
    }

    public void LoadSprite (string spriteName) {
        //this.sprite = Resources.Load<Sprite>("Sprites/Items/" + spriteName);
    }

    public void LoadScriptableObject (string scriptName) {
        this.data = Resources.Load<ItemScriptable> ("Items/" + scriptName);
    }

    public virtual Item CreateInstance () {
        return new Item ();
    }
}