using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interacted : MonoBehaviour {

    protected bool isActive = false;

    [Header ("Label Settings")]
    InteractionLabel labelTemp;

    [SerializeField]
    private string labelText;

    static public UnityEvent _endInteractedEvent = new UnityEvent();

    private void Awake()
    {
        _endInteractedEvent.AddListener(() =>
        {
            EnableUI();
        });
    }

    public void Interact (Transform transform, float height) {
        try {
            InInteraction ();
        } catch { }
        CreateLabel (transform);
        SetLabelPosition (height);
        KeyListener ();
    }

    void CreateLabel (Transform transform) {
        if (labelTemp == null) {
            labelTemp = (InteractionLabel) Instantiate (Resources.Load<InteractionLabel>("PreFabs/InteractionLabel"), transform);
            labelTemp.SetLabel (labelText);
            InteractionRadius.onIntegrate += DestroyIfNotActive;
            try {
                OnStartIntegration ();
            } catch { }
        }
    }

    public void SetLabelText (string text) {
        labelText = text;
    }

    void DestroyIfNotActive (string name) {
        if (name != this.name) {
            try {
                OnCancelIntegration ();
            } catch { }
            DestroyLabel ();
            InteractionRadius.onIntegrate -= DestroyIfNotActive;
        }
    }

    public void DestroyLabel()
    {
        try
        {
            DestroyImmediate(labelTemp.gameObject);
        }
        catch { }
    }

    void SetLabelPosition (float height) {
        labelTemp.transform.position = transform.position + new Vector3 (0, 1.2f * height, 0);
    }

    void KeyListener () {
        if (Input.GetKeyDown (KeyCode.F)) {
            OnInteract ();
            DisableUI();
        }
    }

    private void OnDestroy () {
        try {
            InteractionRadius.onIntegrate -= DestroyIfNotActive;
        } catch { }
    }

    protected void DisableUI()
    {
        UImanager.UIBlock();
    }

    protected void EnableUI()
    {
        UImanager.UIUnlock();
    }

    public virtual void OnInteract () { }
    public virtual void InInteraction () { }
    public virtual void OnStartIntegration () { }
    public virtual void OnCancelIntegration () { }
}