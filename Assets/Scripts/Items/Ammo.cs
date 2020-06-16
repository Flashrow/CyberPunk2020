using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;

[System.Serializable]
public class Ammo : Item {
    public Ammo () : base ("ammo", "Ammo") {
        itemId = "ammo";
        number = 0;
    }

    public Ammo(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }

    public override Item CreateInstance () {
        Ammo newAmmo = new Ammo ();
        newAmmo.cost = cost;
        newAmmo.number = number;
        return newAmmo;
    }
}