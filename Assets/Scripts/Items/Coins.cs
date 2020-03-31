using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : Item
{
    public Coins() : base("coin")
    {
        itemId = "coins";
        defaultNumber = 0;
        minNumber = 1;
        maxNumber = 50;
        number = 0;
        type = ItemType.Coins;
    }

    public override Item CreateInstance()
    {
        return new Coins();
    }
}
