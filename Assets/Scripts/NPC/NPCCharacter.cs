using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCCharacter : MonoBehaviour {
    public float PlayerDetectArea = 10f;
    public float MaxHealth;
    protected float currentHealth { get; set; }
    protected Transform player;
    protected NavMeshAgent agent = null;
    protected NPCMovement movementScript = null;
    protected delegate void AutoDetectUpdateDelegate (float dist);
    protected AutoDetectUpdateDelegate autoDetectUpdate = null;
    void Start () {
        currentHealth = MaxHealth;
        player = PlayerManager.Instance.Player.transform;
        agent = GetComponent<NavMeshAgent> ();
        if (agent != null) {
            movementScript = GetComponent<NPCMovement> ();
            autoDetectUpdate = new AutoDetectUpdateDelegate (isMovable);
        } else autoDetectUpdate = new AutoDetectUpdateDelegate (isStatic);

    }
    protected void Update () {
        float distance = Vector3.Distance (player.position, transform.position);
        autoDetectUpdate (distance);
        if (currentHealth < 0) Die ();
    }
    protected void FaceTarget () {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    protected abstract void isMovable (float dist); // ex. solider - rotate and change position
    protected abstract void isStatic (float dist); // ex. firing turret - only rotate
    public abstract void OnDrawGizmosSelected ();
    public abstract void OnHit (float val);
    protected virtual void Die () { }
}

public class NPCShootedException : System.Exception {
    public NPCShootedException (string ex) {
        Debug.Log (ex);
    }
}