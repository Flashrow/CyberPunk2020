using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathContainer : MonoBehaviour
{
    public string QuestId;
    public string TaskId;

    private int step = 0;
    // Use this for initialization
    void Start()
    {

    }

    public void PullTrigger(Collider other, int order)
    {
        if (other.TryGetComponent<Hero>(out Hero hero) && order == step)
        {
            step++;
            EventListener.instance.Path.Invoke(new PathElementData { order = order, QuestId = this.QuestId, TaskId = this.TaskId });
            Debug.Log($"Step {step}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
