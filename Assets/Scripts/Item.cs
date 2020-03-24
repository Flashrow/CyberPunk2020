using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {Coins, Gun, Ammo};

public class Item : MonoBehaviour
{
    public Sprite sprite;
    public string itemId;
    public int defaultNumber;
    public int minNumber;
    public int maxNumber;
    public int number;
    public GameObject model;
    public ItemType type;
}
