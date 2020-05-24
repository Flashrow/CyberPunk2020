using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEditor;

public class Interaction : MonoBehaviour
{
    protected bool isActive = false;
    
    public InteractionLabel labelTemp;
    [SerializeField] bool disableUI = true;

    [SerializeField]
    private string labelText;

    public UnityEvent OnInteraction = new UnityEvent();
    public UnityEvent InInteraction = new UnityEvent();
    public UnityEvent OnCancelInteraction = new UnityEvent();
    public UnityEvent OnStartInteraction = new UnityEvent();

    public void Awake()
    {
        labelTemp.gameObject.SetActive(false);
        labelTemp.SetLabel(labelText);
    }
    public void Interact()
    {
        InInteraction.Invoke();
        CreateLabel();
        KeyListener();
    }

    void CreateLabel()
    {
        if (labelTemp.gameObject.active == false)
        {
            //labelTemp = (InteractionLabel)Instantiate(Resources.Load<InteractionLabel>("PreFabs/InteractionLabel"), transform);
            labelTemp.gameObject.SetActive(true);
            InteractionRadius.onIntegrate += DestroyIfNotActive;
            OnStartInteraction.Invoke();
        }
    }

    void DestroyIfNotActive(string name)
    {
        if (name != this.name)
        {
            EnableUI();
            OnCancelInteraction.Invoke();
            labelTemp.gameObject.SetActive(false);
            InteractionRadius.onIntegrate -= DestroyIfNotActive;
        }
    }

    void KeyListener()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnInteraction.Invoke();
            if (disableUI) DisableUI();
        }
    }

    private void OnDestroy()
    {
        try
        {
            InteractionRadius.onIntegrate -= DestroyIfNotActive;
        }
        catch { }
    }

    protected void DisableUI()
    {
        UImanager.UIBlock();
    }

    protected void EnableUI()
    {
        UImanager.UIUnlock();
    }
}

[CustomEditor(typeof(Interaction))]
public class InteractionEditor : Editor
{
    string labelText;
    public override void OnInspectorGUI()
    {
        Interaction Target = (Interaction)target;
        if (GUILayout.Button("Create Label"))
        {
            Target.labelTemp = (InteractionLabel)Instantiate(Resources.Load<InteractionLabel>("PreFabs/InteractionLabel"));
            Target.labelTemp.transform.position = Target.transform.position;
            Target.labelTemp.transform.SetParent(Target.transform);
        }
        DrawDefaultInspector();
    }
}
