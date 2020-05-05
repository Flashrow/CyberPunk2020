using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;


[Serializable]
public class DialogueContainer : ScriptableObject
{
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
    public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
    public List<CommentBlockData> CommentBlockData = new List<CommentBlockData>();
    public string questFolder;
    public string fileName;
}


[CustomEditor(typeof(DialogueContainer))]
public class DialogueContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Edit Dialogue"))
        {
            Debug.Log(AssetDatabase.GetAssetPath(Selection.activeInstanceID));
            DialogueContainer Target = (DialogueContainer)target;
            StoryGraph storyGraph = new StoryGraph();
            storyGraph.CreateGraphEditViewWindow(Target.fileName);
            storyGraph.RequestDataOperation(false);
        }
    }
}


