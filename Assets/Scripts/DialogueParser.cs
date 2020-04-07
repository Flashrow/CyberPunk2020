using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueParser : Interacted
{
    [SerializeField] private DialogueContainer dialogue;
    [SerializeField] List<CallbackListItem> callbacks;
    [SerializeField] private DialogueUI dialogueUI;
    private DialogueUI dialogueUITemp;

    [Serializable]
    class CallbackListItem
    {
        [SerializeField] string name;
        [SerializeField] UnityEvent callback;
    }

    string nodeGUID;

    public override void OnInteract()
    {
        if(dialogueUITemp == null)
        {
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

        if (choices.Count > 1)
        {
            var buttons = dialogueUITemp.DisplayChoices(PrepareChoicesList(narrativeDataGUID));
            buttons.ForEach(button =>
            {
                button.onClick.AddListener(() =>
                {
                    var buttonText = button.transform.GetComponentInChildren<Text>().text;
                    dialogueUITemp.ClearButtons();
                    StartCoroutine(ProceedToNarrative(dialogue.DialogueNodeData.Find(x => x.DialogueText == buttonText).NodeGUID));
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
                Debug.Log("koniec");
                yield break;
            }
            StartCoroutine(ProceedToNarrative(GetChoices(nodeGUID).First().TargetNodeGUID));
        }
    }

    private void Update()
    {

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
