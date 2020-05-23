using System;
using System.Reflection;
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
    private float minimalDialogTime = 0f;
    private bool skipDialog = false;
    private DialogueContainer dialogue;
    private DialogueUI dialogueUI;
    private DialogueUI dialogueUITemp;
    private string NPCid;
    private string questId;
    private Hero player = null;

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
        player = GameObject.FindObjectOfType<Hero>();
        dialogueUI = Resources.Load<DialogueUI>("PreFabs/UI/Dialogue/DialogueUI");
    }


    private void Update()
    {
        Debug.Log("Dialogue Parser: player");
        if (
            player.getState() == State.dialog
            && Input.GetKeyDown(KeyCode.Space))
        {
            skipDialog = true;
        }
    }

    public UnityEvent Parse(string NPCid, string questId)
    {
        if (dialogueUITemp == null)
        {
            player.setState(State.dialog);
            this.NPCid = NPCid;
            this.questId = questId;
            dialogue = dialogues.GetDialogue(QuestManager.instance.Quests[questId].dialoguesQueue[NPCid].Dequeue());
            dialogueUITemp = (DialogueUI)Instantiate(dialogueUI);
            var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
            EventListener.instance.Dialogues.Invoke(new DialogData{NpcID = this.NPCid});
            StartCoroutine(ProceedToNarrative(narrativeData.TargetNodeGUID));
            return onEndDialog;
        }
        return onEndDialog;
    }

    private IEnumerator ProceedToNarrative(string narrativeDataGUID)
    {
        Debug.Log("Dialogue Parser: ProceedToNarrative");
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

        callbacks.Where(callback => callback.name == GetDialogueNode(narrativeDataGUID).Callback).ToList().ForEach(callback => callback.callback.Invoke());
        AddNextDialogueIfExists(narrativeDataGUID);// Dodaje kolejny  dialog to kolejki jesli istnieje
        RunQuestMethodIfExists(narrativeDataGUID);// Uruchamia metode w activeQuest;

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

            for(int i = 0; i <= 50; i++)
            {
                yield return new WaitForSeconds((minimalDialogTime + speed * text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length)/50);
                if (skipDialog)
                {
                    skipDialog = false;
                    break;
                }
            }
           
            if (GetChoices(nodeGUID).Count == 0)
            {
                dialogueUITemp.FinishDialogue();
                player.setState(State.playing);
                onEndDialog.Invoke();
                yield break;
            }
            StartCoroutine(ProceedToNarrative(GetChoices(nodeGUID).First().TargetNodeGUID));
        }
    }

    private void AddNextDialogueIfExists(string narrativeDataGUID)
    {
        if (dialogues.GetDialogue(GetDialogueNode(narrativeDataGUID).Callback))
        {
           QuestManager.instance.Quests[this.questId].dialoguesQueue[NPCid].Enqueue(GetDialogueNode(narrativeDataGUID).Callback);
        }
    }

    private void RunQuestMethodIfExists(string narrativeDataGUID)
    {
        string methodName = GetDialogueNode(narrativeDataGUID).Callback;
        MethodInfo mi = QuestManager.instance.Quests[this.questId].GetType().GetMethod(methodName);
        try
        {
            mi.Invoke(QuestManager.instance.Quests[this.questId], null);
        } catch { };
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
