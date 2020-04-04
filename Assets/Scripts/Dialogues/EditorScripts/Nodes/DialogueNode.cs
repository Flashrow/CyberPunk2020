using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class DialogueNode : Node
{
    public string DialogueText;
    public string GUID;
    public string Player;
    public string Callback;
    public bool EntyPoint = false;
}
