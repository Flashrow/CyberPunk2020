using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {
    public Transform[] MoveSpots;
    protected Nullable<int> randomSpot = null;
    public NavMeshAgent Agent { get; private set; }
    void Awake () {
        Agent = GetComponent<NavMeshAgent> ();
    }
    void Start () {
        if (MoveSpots.Length > 0)
            randomSpot = UnityEngine.Random.Range (0, MoveSpots.Length);
    }
    public void SetSpeed (float speed) {
        Agent.speed = speed;
    }
    /**
    Speed must between [1, 5)
    */
    public void Walking (float speed = 3f) {
        Agent.speed = speed;
    }
    public void Idle () {
        Agent.speed = 0;
    }
    /**
    Speed must between (5, ...)
    */
    public void Running (float speed = 6f) {
        Agent.speed = speed;
    }
    public void Spot (float speed) {
        if (randomSpot.HasValue) {
            SetSpeed (speed);
            Agent.SetDestination (MoveSpots[randomSpot.Value].position);
            if (Vector3.Distance (transform.position, MoveSpots[randomSpot.Value].position) < Agent.stoppingDistance)
                randomSpot = UnityEngine.Random.Range (0, MoveSpots.Length);
        }
    }

    public void Spot () {
        if (randomSpot.HasValue) {
            Walking ();
            Agent.SetDestination (MoveSpots[randomSpot.Value].position);
            if (Vector3.Distance (transform.position, MoveSpots[randomSpot.Value].position) < Agent.stoppingDistance)
                randomSpot = UnityEngine.Random.Range (0, MoveSpots.Length);
        }
    }
}