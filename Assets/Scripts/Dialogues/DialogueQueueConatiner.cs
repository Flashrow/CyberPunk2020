using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueContainer", menuName = "DialogueContainer")]
public class DialoguesConatiner : ScriptableObject
{
    [System.Serializable]
    class DialoguesConatinerListItem
    {
        [SerializeField] public string questId;
        [SerializeField] public DialogueContainer dialogue;
    }

    [SerializeField] List<DialoguesConatinerListItem> questDialoguesList = new List<DialoguesConatinerListItem>();
    public Dictionary<string, Queue<DialogueContainer>> questDialogues = new Dictionary<string, Queue<DialogueContainer>>();

    private void Awake()
    {
        questDialoguesList.ForEach(item =>
        {
            questDialogues[item.questId].Enqueue(item.dialogue);
        });
    }
   
    public Queue<DialogueContainer> GetQuestDialogues(string questId)
    {
        return questDialogues[questId];
    }

    public void RemoveDialog(string questId)
    {
        questDialogues[questId].Dequeue();
    }
}
