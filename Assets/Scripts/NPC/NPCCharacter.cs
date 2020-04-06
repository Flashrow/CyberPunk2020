using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCCharacter : MonoBehaviour {
    protected float currentHealth { get; set; }
    protected Transform player;
    protected NPCMovement movementScript = null;
    protected delegate void OnUpdateDelegate (float dist);
    protected OnUpdateDelegate onUpdate = null;
    void Awake () {
        movementScript = GetComponent<NPCMovement> ();
        if (movementScript != null) {
            onUpdate = new OnUpdateDelegate (isMovable);
        } else onUpdate = new OnUpdateDelegate (isStatic);
        onAwake ();
    }
    void Start () {
        player = PlayerManager.Instance.Player.transform;
        onStart ();
    }
    protected void Update () {
        float distance = Vector3.Distance (player.position, transform.position);
        try {
            onUpdate (distance);
        } catch (NPCDie) {
            onDie ();
        }
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
    protected virtual void onDie () { }
    protected virtual void onAwake () { }
    protected virtual void onStart () { }
}

public class NPCShootedException : System.Exception {
    public NPCShootedException (string ex) {
        Debug.Log (ex);
    }
}

public class NPCDie : System.Exception { }