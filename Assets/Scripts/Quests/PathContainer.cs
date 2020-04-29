using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PathContainer : MonoBehaviour
{
    //public string QuestId;
    //public string TaskId;
    public class PathEventHandler : UnityEvent<Collider, int> { };
    public PathEventHandler pathEventHandler;
    
    // Use this for initialization

    private void Awake()
    {
        if (pathEventHandler == null) pathEventHandler = new PathEventHandler();
    }
    void Start()
    {

    }

    public void PullTrigger(Collider other, int order)
    {
        pathEventHandler.Invoke(other, order);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
