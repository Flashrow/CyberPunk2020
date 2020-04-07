using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueParser : MonoBehaviour
{
    [SerializeField] List<CallbackListItem> callbacks;
    [SerializeField] private DialoguesConatiner dialogues;
    private DialogueContainer dialogue;
    private DialogueUI dialogueUI;
    private DialogueUI dialogueUITemp;

    [NonSerialized] public UnityEvent onEndDialog = new UnityEvent();

    [Serializable]
    class CallbackListItem
    {
        [SerializeField] public string name;
        [SerializeField] public UnityEvent callback;
    }

    string nodeGUID;

    private void Awake()
    {
        dialogueUI = Resources.Load<DialogueUI>("PreFabs/UI/Dialogue/DialogueUI");
    }

    public void Parse(string questId)
    {
        if (dialogueUITemp == null)
        {
            dialogue = dialogues.GetQuestDialogues(questId).Peek();
            dialogueUITemp = (DialogueUI)Instantiate(dialogueUI);
            var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
            StartCoroutine(ProceedToNarrative(narrativeData.TargetNodeGUID));
        }
    }

    private IEnumerator ProceedToNarrative(string narrativeDataGUID)
    {
        nodeGUID = narrativeDataGUID;
        var text = GetDialogueNode(narrativeDataGUID).DialogueText;
        var spekaer = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).Speaker;
        var choices = GetChoices(narrativeDataGUID);
        switch (spekaer)
        {
            case "NPC":
                dialogueUITemp.DisplayNpcText(text);
                break;
            case "Player":
                dialogueUITemp.DisplayPlayerText(text);
                break;
        }
        //callbacks.Where(callback => callback.name == GetDialogueNode(narrativeDataGUID).Callback).ToList().ForEach(callback => callback.callback.Invoke());
        if (choices.Count > 1)
        {
            var buttons = dialogueUITemp.DisplayChoices(GetChoices(narrativeDataGUID));
            buttons.ForEach(button =>
            {
                button.onClick.AddListener(() =>
                {
                    var buttonText = button.transform.GetComponentInChildren<Text>().text;
                    dialogueUITemp.ClearButtons();
                    StartCoroutine(ProceedToNarrative(GetChoices(narrativeDataGUID).Find(node => node.PortName == buttonText).TargetNodeGUID));
                });
            });
        } else
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            float speed = (60f / 100f);
            yield return new WaitForSeconds(speed*text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length);
            if (GetChoices(nodeGUID).Count == 0)
            {
                dialogueUITemp.FinishDialogue();
                onEndDialog.Invoke();
                yield break;
            }
            StartCoroutine(ProceedToNarrative(GetChoices(nodeGUID).First().TargetNodeGUID));
        }
    }

    private DialogueNodeData GetDialogueNode(string id)
    {
        return dialogue.DialogueNodeData.Find(x => x.NodeGUID == id);
    }

    private List<NodeLinkData> GetChoices(string id)
    {
        return dialogue.NodeLinks.Where(x => x.BaseNodeGUID == id).ToList();
    }

    private List<DialogueNodeData> PrepareChoicesList(string id)
    {
        var list = new List<DialogueNodeData>();
        GetChoices(id).ForEach(nodeLinkData =>
        {
            list.Add(GetDialogueNode(nodeLinkData.TargetNodeGUID));
        });
        return list;
    }

    private string ProcessProperties(string text)
    {
        foreach (var exposedProperty in dialogue.ExposedProperties)
        {
            text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
        }
        return text;
    }
}
