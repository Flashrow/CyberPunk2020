using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools : Item
{
    public Tools() : base("tools")
    {
        itemId = "tools";
        defaultNumber = 0;
        minNumber = 1;
        maxNumber = 5;
        number = 0;
        type = ItemType.Tools;
    }

    public override Item CreateInstance()
    {
        return new Tools();
    }
}
