using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class NPCQuestInteractions : Interacted {
    bool isActive = false;
    public string NpcId;
    public CinemachineVirtualCamera QuestCamera;
    private NPCQuestAnimation anim = null;
    void Awake () {
        anim = GetComponentInChildren<NPCQuestAnimation> ();
    }
    void Update () {
        if (isActive) {
            if (Input.GetKeyDown (KeyCode.Escape)) {
                isActive = false;
                QuestCamera.Priority = 0;
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
        QuestCamera.Priority = 100;
    }

    public override void InInteraction () { }
    public override void OnStartIntegration () { }
    public override void OnCancelIntegration () { }
};