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
        [SerializeField] public string dialogueId;
        [SerializeField] public DialogueContainer dialogue;
    }

    [SerializeField] List<DialoguesConatinerListItem> questDialoguesList = new List<DialoguesConatinerListItem>();
    public Dictionary<string, DialogueContainer> questDialogues = new Dictionary<string, DialogueContainer>();

    public void OnEnable()
    {
        questDialoguesList.ForEach(item =>
        {
            if (questDialogues.ContainsKey(item.dialogueId))
            {
                //questDialogues.Add(item.dialogueId,item.dialogue);
            }
            else
            {
                questDialogues.Add(item.dialogueId, item.dialogue);
            }
        });
    }
   
    public DialogueContainer GetDialogue(string dialogueId)
    {
        if (questDialogues.ContainsKey(dialogueId))
            return questDialogues[dialogueId];
        else return null;
    }

    public void RemoveDialog(string dialogueId)
    {
        questDialogues.Remove(dialogueId);
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