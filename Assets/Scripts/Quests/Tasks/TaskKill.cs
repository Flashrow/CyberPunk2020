using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TaskKill", menuName = "Tasks/Kill", order = 2), System.Serializable]
public class TaskKill : Task
{
    [SerializeField] private List<string> list = new List<string>();
    
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Run()
    {
        EventListener.instance.Kills.AddListener(kill =>
       {
           if (list.Contains(kill.NpcId))
           {
               list.Remove(kill.NpcId);
               if (list.Count == 0) Finish();
           }
       });
    }

    public override void Finish()
    {
        status = QuestStatus.DONE;
        onFinish.Invoke();
    }
}
