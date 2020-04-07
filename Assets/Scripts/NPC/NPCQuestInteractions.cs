using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestInteractions : Interacted {
    bool isActive = false;
    public GameObject QuestCamera;
    private NPCQuestAnimation anim = null;
    void Awake () {
        anim = GetComponentInChildren<NPCQuestAnimation> ();
    }
    void Update () {
        if (isActive) {
            if (Input.GetKeyDown (KeyCode.Escape)) {
                isActive = false;
                QuestCamera.SetActive (false);
                CameraManager.Instance.Current.gameObject.SetActive (true);
                PlayerManager.Instance.Player.GetComponent<CharacterController> ().enabled = true;
                anim.AnimEndInteraction ();
            }
            // TODO Kamil: Interactions
        }
    }

    public override void OnInteract () {
        if (isActive) return;
        PlayerManager.Instance.Player.GetComponent<CharacterController> ().enabled = false;
        anim.AnimStartInteraction ();
        isActive = true;
        CameraManager.Instance.Current.gameObject.SetActive (false);
        QuestCamera.SetActive (true);
    }

    public override void InInteraction () { }
    public override void OnStartIntegration () { }
    public override void OnCancelIntegration () { }
};