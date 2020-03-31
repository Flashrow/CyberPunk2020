﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : Item
{
   public Ammo(): base("ammo", "Ammo")
   {
        itemId = "ammo";
        number = 0;
    }

    public override Item CreateInstance()
    {
        return new Ammo();
    }
}
