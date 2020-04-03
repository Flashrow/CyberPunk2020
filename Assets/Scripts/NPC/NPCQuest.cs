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
    void Update () {
        float distance = Vector3.Distance (player.position, transform.position);
        if (distance <= PlayerDetectArea) {
            // agent.SetDestination (player.position);
            FaceTarget ();
            if (distance <= agent.stoppingDistance) {
                NPCQuestInteraction ();
            }
        } else NPCMove ();
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
    public override void OnHit (int val) {
        throw new NPCShootedException ("YOU HIT: NPC_QUEST");
    }
}