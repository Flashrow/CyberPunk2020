using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;

[System.Serializable]
public class Phone : Item {
    public Phone () : base ("phone", "Phone") {
        itemId = "phone";
        number = 0;
    }

    public Phone(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
    public override Item CreateInstance () {
        Phone newPhone = new Phone ();
        newPhone.cost = cost;
        newPhone.number = number;
        return newPhone;
    }
}