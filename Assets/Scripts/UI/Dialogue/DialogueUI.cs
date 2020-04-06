using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Text npcText;
    [SerializeField] private Text playerText;
    [SerializeField] private Button choiceButton;
    [SerializeField] private Transform buttonsContainer;
    // Use this for initialization
    void Start()
    {

    }

    public List<Button> DisplayChoices(List<DialogueNodeData> choices)
    {
        var list = new List<Button>();
        choices.ForEach(choice =>
        {
            list.Add((Button)Instantiate(choiceButton, buttonsContainer));
            list[list.Count - 1].transform.GetComponentInChildren<Text>().text = choice.DialogueText;
        });
        return list;
    }

    public void DisplayNpcText(string text)
    {
        ClearTexts();
        var npc = Instantiate(npcText, transform);
        npc.text = text;
    }

    public void DisplayPlayerText(string text)
    {
        ClearTexts();
        var npc = Instantiate(playerText, transform);
        npc.text = text;
    }

    void ClearTexts()
    {
        var texts = transform.GetComponentsInChildren<Text>();
        foreach(var text in texts)
        {
            DestroyImmediate(text.gameObject);
        }
    }

    public void ClearButtons()
    {
        var buttons = buttonsContainer.gameObject.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            DestroyImmediate(buttons[i].gameObject);
        }
    }

    public void FinishDialogue()
    {
        DestroyImmediate(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
