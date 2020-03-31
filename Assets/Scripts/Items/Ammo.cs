using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : Item
{
   public Ammo(): base("ammo")
   {
        itemId = "ammo";
        defaultNumber = 0;
        minNumber = 1;
        maxNumber = 100;
        number = 0;
        type = ItemType.Ammo;
    }

    public override Item CreateInstance()
    {
        return new Ammo();
    }
}
