using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCQuestInteractions : Interacted {
    bool isActive = false;
    public string NpcId;
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
        EventListener.instance.Interaction.Invoke(new InteractionData { NpcId = this.NpcId, gameObject = this.gameObject });
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