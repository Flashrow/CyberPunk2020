using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {
    public Transform[] MoveSpots;
    protected Nullable<int> randomSpot = null;
    void Start () {
        if (MoveSpots.Length > 0)
            randomSpot = UnityEngine.Random.Range (0, MoveSpots.Length);
    }
    public void Spot (NavMeshAgent agent) {
        if (randomSpot.HasValue) {
            agent.SetDestination (MoveSpots[randomSpot.Value].position);
            if (Vector3.Distance (transform.position, MoveSpots[randomSpot.Value].position) < agent.stoppingDistance)
                randomSpot = UnityEngine.Random.Range (0, MoveSpots.Length);
        }
    }
}