using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuest : NPCCharacter {
    public GameObject QuestCamera;
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    void NPCQuestInteraction () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            QuestCamera.SetActive (false);
            CameraManager.Instance.Current.gameObject.SetActive (true);
        }
        if (Input.GetKeyDown (KeyCode.F)) {
            Debug.Log ("NPC QUEST INTERACTION");
            CameraManager.Instance.Current.gameObject.SetActive (false);
            QuestCamera.SetActive (true);
        }
    }
    public override void OnHit (float val) {
        throw new NPCShootedException ("YOU HIT: NPC_QUEST");
    }

    protected override void isMovable (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
            if (dist <= agent.stoppingDistance) {
                agent.SetDestination (player.position);
                NPCQuestInteraction ();
            }
        } else movementScript.Spot (agent);
    }
    protected override void isStatic (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
            if (dist <= 5) NPCQuestInteraction ();
        }
    }
}