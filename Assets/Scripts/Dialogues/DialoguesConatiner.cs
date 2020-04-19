using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(fileName = "DialogueContainer", menuName = "DialogueContainer")]
public class DialoguesConatiner : ScriptableObject
{
    [System.Serializable]
    public class DialoguesConatinerListItem
    {
        [SerializeField] public string questId;
        [SerializeField] public DialogueContainer dialogue;
    }

    [SerializeField] List<DialoguesConatinerListItem> questDialoguesList = new List<DialoguesConatinerListItem>();
    public Dictionary<string, Queue<DialogueContainer>> questDialogues = new Dictionary<string, Queue<DialogueContainer>>();

    public void OnEnable()
    {
        questDialoguesList.ForEach(item =>
        {
            if (questDialogues.ContainsKey(item.questId))
            {
                questDialogues[item.questId].Enqueue(item.dialogue);
            }
            else
            {
                questDialogues.Add(item.questId, new Queue<DialogueContainer>());
                questDialogues[item.questId].Enqueue(item.dialogue);
            }
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

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
    [CustomEditor(typeof(DialoguesConatiner))]
    public class MultiPropertySampleEditor : UnityEditor.Editor
    {
        private ReorderableList reorderableList;

        private void OnEnable()
        {
            reorderableList = new ReorderableList(
                serializedObject.FindProperty("questDialoguesList")
            );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //DrawDefaultInspector();
            EditorGUI.BeginChangeCheck();
            {
                EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

                if (reorderableList != null)
                    reorderableList.Layout();
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}