using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuest : NPCCharacter {
    public float DetectPlayerRadius = 7f;
    private NPCQuestAnimation anim = null;
    protected override void onAwake () {
        anim = GetComponentInChildren<NPCQuestAnimation> ();
    }
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position, DetectPlayerRadius);
    }
    public override void OnHit (float val) {
        anim.AnimHit ();
        throw new NPCShootedException ("YOU HIT: NPC_QUEST");
    }
    protected override void isMovable (float dist) {
        anim.AnimSetSpeed ((int) movementScript.Agent.speed);
        if (dist <= DetectPlayerRadius) {
            FaceTarget ();
            movementScript.Idle ();
        } else movementScript.Spot ();
    }
    protected override void isStatic (float dist) {
        if (dist <= DetectPlayerRadius) {
            FaceTarget ();
        }
    }
}