using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHumanoid : NPCCharacter {
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    void Update () {
        float distance = Vector3.Distance (player.position, transform.position);
        if (distance <= PlayerDetectArea) {
            FaceTarget ();
            if (distance <= agent.stoppingDistance) {
                agent.SetDestination (player.position);
            }
        } else NPCMove ();
    }
    public override void OnHit (int val) {
        throw new NPCShootedException ("YOU HIT: NPC_HUMANOID");
    }
}