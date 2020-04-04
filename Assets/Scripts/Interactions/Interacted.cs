using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacted : MonoBehaviour {
    [SerializeField, Header ("Label Settings")]
    InteractionLabel label;
    InteractionLabel labelTemp;

    [SerializeField]
    private string labelText;

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
            labelTemp = (InteractionLabel) Instantiate (label, transform);
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

    public void DestroyLabel () {
        try {
            DestroyImmediate (labelTemp.gameObject);
        } catch { }
    }

    void SetLabelPosition (float height) {
        labelTemp.transform.position = transform.position + new Vector3 (0, 1.2f * height, 0);
    }

    void KeyListener () {
        if (Input.GetKeyDown (KeyCode.F)) {
            OnInteract ();
        }
    }

    private void OnDestroy () {
        try {
            InteractionRadius.onIntegrate -= DestroyIfNotActive;
        } catch { }
    }

    public virtual void OnInteract () { }
    public virtual void InInteraction () { }
    public virtual void OnStartIntegration () { }
    public virtual void OnCancelIntegration () { }
}