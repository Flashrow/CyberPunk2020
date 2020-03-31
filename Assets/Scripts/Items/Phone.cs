using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : Item
{
    public Phone() : base("phone", "Phone")
    {
        itemId = "phone";
        number = 0;
    }

    public override Item CreateInstance()
    {
        return new Phone();
    }
}
