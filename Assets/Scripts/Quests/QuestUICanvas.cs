using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestUICanvas : MonoBehaviour
{
    Transform questsBox = null, tasksContent = null;

    Text descriptionTitle = null, descriptionContent = null;

    void LoadData()
    {
        if (QuestManager.instance.Quests.Count > 0)
        {
            foreach(KeyValuePair<string, Quest> entry in QuestManager.instance.Quests)
            {
                if (entry.Value.isActive == false) continue;
                Debug.Log("Some active quest");
                GameObject go = Resources.Load("PreFabs/UI/Quests/QuestBtn") as GameObject;
                GameObject clone = Instantiate(go);
                clone.transform.parent = questsBox;
                clone.transform.localScale = new Vector3Int(1, 1, 1);
                if (entry.Value.data.title == null)
                    clone.GetComponentInChildren<Text>().text = "";
                else
                    clone.GetComponentInChildren<Text>().text = entry.Value.data.title;
                clone.GetComponent<Button>().onClick.AddListener(() =>
                {
                    this.ShowQuestContent(entry.Value.data);
                });
            }
            foreach (KeyValuePair<string, Quest> entry in QuestManager.instance.Quests)
            {
                if(entry.Value.isActive) { 
                    this.ShowQuestContent(entry.Value.data);
                    break;
                }
            }
        }
    }

    public void ShowQuestContent(QuestData qd)
    {
        ShowQuestTask(ref qd.tasks);
        if (qd.title == null)
            descriptionTitle.text = "";
        else
            descriptionTitle.text = qd.title;
        if (qd.description == null)
            descriptionTitle.text = "";
        else
            descriptionContent.text = qd.description;
    }

    void ShowQuestTask(ref List<Task> qd)
    {
        foreach (Transform child in tasksContent)
        {
            Destroy(child.gameObject);
        }
        qd.ForEach(el =>
        {
            GameObject go = null;
            switch (el.status)
            {
                case QuestStatus.DONE:
                    go = Resources.Load("PreFabs/UI/Quests/TaskItemDone") as GameObject;
                    break;
                case QuestStatus.TODO:
                    go = Resources.Load("PreFabs/UI/Quests/TaskItemTodo") as GameObject;
                    break;
                case QuestStatus.IN_PROGRESS:
                    go = Resources.Load("PreFabs/UI/Quests/TaskItemTodo") as GameObject;
                    break;
            }
            GameObject clone = Instantiate(go);
            clone.transform.parent = tasksContent;
            clone.transform.localScale = new Vector3Int(1, 1, 1);
            clone.GetComponentInChildren<Text>().text = el.title;
            clone.GetComponent<Button>().onClick.AddListener(() => {
                descriptionTitle.text = el.title;
                descriptionContent.text = el.description;
            });
        });
    }

    void OnEnable()
    {
        if(questsBox == null)
        {
            questsBox = GameObject.Find("QuestsBox/").transform;
            tasksContent = GameObject.Find("TasksBox/Content").transform;
            descriptionTitle = GameObject.Find("DescriptionBox/Title").GetComponent<Text>();
            descriptionContent = GameObject.Find("DescriptionBox/Content").GetComponent<Text>();
        }
        try { 
            LoadData();
        }
        catch(Exception what)
        {
            Debug.LogError($"{what}. Are there some null ScriptableObject in QuestManager?");
        }
    }

    void OnDisable()
    {
        foreach (Transform child in questsBox)
            Destroy(child.gameObject);
        
        foreach (Transform child in tasksContent)
            Destroy(child.gameObject);

        descriptionTitle.text = "";
        descriptionContent.text = "";
    }

}
