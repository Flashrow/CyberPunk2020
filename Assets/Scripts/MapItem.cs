using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Interaction))]
public class MapItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public ItemScriptable item;
    [SerializeField] int number = 1;

    public void OnInteract()
    {
        Item _item = Item.CreateItemObjectByType(item.type);
        _item.number = number;
        PlayerManager.Instance.HeroScript.inventory.AddItem(_item);
        EventListener.instance.Inventory.Invoke(new ItemData {
            item = _item,
            eventType = ItemEventType.TAKEN,
        });
        Destroy(gameObject);
    }    
}

#if UNITY_EDITOR
[CustomEditor(typeof(MapItem))]
public class ItemScriptableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create GameObject"))
        {
            MapItem Target = (MapItem)target;
            GameObject temp = (GameObject)Instantiate(Target.item.model, Target.transform);
        }
    }
}
#endif
