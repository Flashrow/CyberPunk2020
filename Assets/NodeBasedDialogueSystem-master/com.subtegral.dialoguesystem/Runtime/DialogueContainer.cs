using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DialogueContainer : ScriptableObject
{
    public List<Subtegral.DialogueSystem.DataContainers.NodeLinkData> NodeLinks = new List<Subtegral.DialogueSystem.DataContainers.NodeLinkData>();
    public List<Subtegral.DialogueSystem.DataContainers.DialogueNodeData> DialogueNodeData = new List<Subtegral.DialogueSystem.DataContainers.DialogueNodeData>();
    public List<Subtegral.DialogueSystem.DataContainers.ExposedProperty> ExposedProperties = new List<Subtegral.DialogueSystem.DataContainers.ExposedProperty>();
    public List<Subtegral.DialogueSystem.DataContainers.CommentBlockData> CommentBlockData = new List<Subtegral.DialogueSystem.DataContainers.CommentBlockData>();
}
