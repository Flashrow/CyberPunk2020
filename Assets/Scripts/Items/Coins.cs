using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;

[System.Serializable]
public class Coins : Item {
    public Coins () : base ("coin", "Coin") {
        itemId = "coins";
        number = 0;
    }

    public Coins(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
    public override Item CreateInstance () {
        Coins newCoins = new Coins ();
        newCoins.number = number;
        newCoins.cost = cost;
        return newCoins;
    }
}