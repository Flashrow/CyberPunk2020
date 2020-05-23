using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "TaskTalkTo", menuName = "Tasks/Talk to", order = 4), System.Serializable]
public class TaskTalkTo : Task
{
    [SerializeField] private string NpcID;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Run()
    {
        status = QuestStatus.IN_PROGRESS;
        EventListener.instance.Dialogues.AddListener(Data =>
        {
            if(Data.NpcID == NpcID)
            {
                Finish();
            }
        });
    }

    public override void Finish()
    {
        status = QuestStatus.DONE;
        onFinish.Invoke();
    }
}