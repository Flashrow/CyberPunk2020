using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Linq;

public class GraphSaveUtility : MonoBehaviour
{
    private DialogueGraphView _targetGraphView;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!Edges.Any()) return;

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();


        foreach (var dialogueNode in Nodes.Where(node => !node.EntryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
            {
                NodeGUID = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position,
            });
        }
        Debug.Log("dialogueContainer.DialogueNodeData.Count");
        Debug.Log(dialogueContainer.DialogueNodeData.Count);

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for(int i = 0; i <connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            dialogueContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGUID = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGUID = inputNode.GUID
            });
        }
        


        //if (!AssetDatabase.IsValidFolder("Assets/Resources/Dialogues/"))
        //    AssetDatabase.CreateFolder("Resources", "Dialogues");

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/Dialogues/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {

    }
}
