using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCShootedException : System.Exception {
    public NPCShootedException (string ex) {
        Debug.Log (ex);
    }
}

public abstract class NPCCharacter : MonoBehaviour {
    public float PlayerDetectArea = 10f;
    public Transform[] MoveSpots;
    public float MaxHealth;
    protected float currentHealth { get; set; }
    protected int randomSpot;
    protected Transform player;
    protected NavMeshAgent agent;
    void Start () {
        currentHealth = MaxHealth;
        player = PlayerManager.Instance.Player.transform;
        agent = GetComponent<NavMeshAgent> ();
        if (MoveSpots.Length > 0)
            randomSpot = UnityEngine.Random.Range (0, MoveSpots.Length);
    }
    protected void FaceTarget () {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    protected virtual void NPCMove () {
        if (MoveSpots.Length > 0) {
            agent.SetDestination (MoveSpots[randomSpot].position);
            if (
                Vector3.Distance (transform.position, MoveSpots[randomSpot].position) <
                agent.stoppingDistance + 1
            )
                randomSpot = UnityEngine.Random.Range (0, MoveSpots.Length);
        }
    }
    public abstract void OnDrawGizmosSelected ();
    public abstract void OnHit (float val);
    protected virtual void Die () { }
}