using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


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

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueContainer))]
public class DialogueContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Edit Dialogue"))
        {
            DialogueContainer Target = (DialogueContainer)target;
            StoryGraph storyGraph = new StoryGraph();
            storyGraph.CreateGraphEditViewWindow(Target.fileName, AssetDatabase.GetAssetPath(Selection.activeInstanceID));
            storyGraph.RequestDataOperation(false);
        }
    }
}
#endif


