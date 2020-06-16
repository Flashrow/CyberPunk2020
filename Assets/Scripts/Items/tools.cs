using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;

[System.Serializable]
public class Tools : Item, ISerializable{
    public Tools () : base ("tools", "Tools") {
        itemId = "tools";
        number = 0;
    }

    public Tools(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }

    public override Item CreateInstance () {
        Tools newTools = new Tools ();
        newTools.cost = cost;
        newTools.number = number;
        return newTools;
    }
}