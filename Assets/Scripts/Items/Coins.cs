using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : Item {
    public Coins () : base ("coin", "Coin") {
        itemId = "coins";
        number = 0;
    }

    public override Item CreateInstance()
    {
        Coins newCoins = new Coins();
        newCoins.number = number;
        newCoins.cost = cost;
        return newCoins;
    }
}