using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : NPCCharacter {

    public ushort maxHealth;
    public ushort currentHealth { get; protected set; }
    public NPCEnemy () {
        currentHealth = maxHealth;
    }
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    void Update () {
        float distance = Vector3.Distance (player.position, transform.position);
        if (distance <= PlayerDetectArea) {
            agent.SetDestination (player.position);
            FaceTarget ();
            if (distance <= agent.stoppingDistance) {
                Debug.Log ("NPC ENEMY FIGHT");
            }
        }
    }
    public override void Die () {
        if (currentHealth < 0) {
            Destroy (gameObject);
            Debug.Log ("NPC ENEMY DIE");
        }
    }
}