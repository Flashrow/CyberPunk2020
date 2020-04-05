using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHumanoid : NPCCharacter {
    private NPCAnimation anim = null;
    protected override void onAwake () {
        anim = GetComponentInChildren<NPCAnimation> ();
    }
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, movementScript.Agent.stoppingDistance);
    }
    public override void OnHit (float val) {
        anim.AnimHit ();
        throw new NPCShootedException ("YOU HIT: NPC_HUMANOID");
    }
    protected override void isMovable (float dist) {
        anim.AnimSetSpeed ((int) movementScript.Agent.speed);
        movementScript.Spot ();
        if (dist <= movementScript.Agent.stoppingDistance)
        {
             FaceTarget ();
             movementScript.Idle ();
        }
    }
    protected override void isStatic (float dist) {
        if (dist <= movementScript.Agent.stoppingDistance) {
            FaceTarget ();
        };
    }
}