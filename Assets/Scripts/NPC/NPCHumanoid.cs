using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHumanoid : NPCCharacter {
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    public override void OnHit (float val) {
        throw new NPCShootedException ("YOU HIT: NPC_HUMANOID");
    }

    protected override void isMovable (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
        } else movementScript.Spot (agent);
    }
    protected override void isStatic (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
        };
    }
}