using UnityEngine;
using System.Collections;

public class Quest1 : Quest
{
    // Use this for initialization
    void Start()
    {
        Debug.Log(data.description);
        RunTasksQueue(0);
    }

    private void RunTasksQueue(int i)
    {
        if (data.tasks.Count > i)
        {
            data.tasks[i].Run();
            data.tasks[i].onFinish.AddListener(() =>
            {
                RunTasksQueue(i+1);
                Debug.Log("Task finished");
            });
        } else
        {
            //Finish Quest
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PlayerAboartQuest()
    {
        throw new System.NotImplementedException();
    }

    public override void PlayerAcceptQuest()
    {
        throw new System.NotImplementedException();
    }
}
