using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuest : NPCCharacter {
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    public override void OnHit (float val) {
        throw new NPCShootedException ("YOU HIT: NPC_QUEST");
    }
    protected override void isMovable (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
            if (dist <= agent.stoppingDistance) {
                agent.SetDestination (player.position);
            }
        } else movementScript.Spot (agent);
    }
    protected override void isStatic (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
        }
    }
}