using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : Item
{
    public Phone() : base("phone")
    {
        itemId = "phone";
        defaultNumber = 0;
        minNumber = 1;
        maxNumber = 1;
        number = 0;
        type = ItemType.Phone;
    }
}
