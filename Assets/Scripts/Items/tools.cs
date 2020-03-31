using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools : Item
{
    public Tools() : base("tools", "Tools")
    {
        itemId = "tools";        
        number = 0;
    }

    public override Item CreateInstance()
    {
        return new Tools();
    }
}
