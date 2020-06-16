using UnityEngine;
using System.Collections;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Interaction : MonoBehaviour
{
    protected bool isActive = false;
    
    public InteractionLabel labelTemp;
    bool disableUI = true;

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
            UImanager.UIUnlock();
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
            if (disableUI) UImanager.UIBlock();
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
}

#if UNITY_EDITOR
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
#endif