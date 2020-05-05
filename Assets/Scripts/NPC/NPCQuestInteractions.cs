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
    [SerializeField] QuestData questData = null;
    void Awake () {
        anim = GetComponentInChildren<NPCQuestAnimation> ();
    }
    void Update () {
        if (isActive) {
            if (Input.GetKeyDown (KeyCode.Escape)) {
                OnEscape();
            }
        }
    }

    void OnEscape()
    {
        QuestCamera.Priority = 0;
        anim.AnimEndInteraction();
        MinimapEvents.TurnOn.Invoke();
        PlayerManager.EnableMovement.Invoke();
        isActive = false;
    }

    public override void OnInteract () {
        if (isActive) return;
        QuestManager.instance.MountQuest(questData);
        EventListener.instance.Interaction.Invoke(new InteractionData {
            NpcId = this.NpcId,
            gameObject = this.gameObject,
            EndInteraction = () => OnEscape(),
            DialogueParser = gameObject.GetComponent<DialogueParser>()
        });
        gameObject.GetComponent<DialogueParser>().Parse(NpcId, questData.QuestId).AddListener(() =>
        {
            OnEscape();
            Debug.Log("Koniec Dialogu");
        });
        PlayerManager.DisableMovement.Invoke();
        QuestCamera.Priority = 100;
        MinimapEvents.TurnOff.Invoke();
        anim.AnimStartInteraction();
        isActive = true;
    }

    public override void InInteraction () { }
    public override void OnStartIntegration () { }
    public override void OnCancelIntegration () { }
};