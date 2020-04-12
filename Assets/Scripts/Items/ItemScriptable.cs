using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Slots { Secondary, Primary }

[CreateAssetMenu (fileName = "ItemData", menuName = "Inventory/Item")]
public class ItemScriptable : ScriptableObject {
    public string itemId;
    public int defaultNumber = 0;
    public int minNumber = 0;
    public int maxNumber = 100;
    public Sprite sprite;
    public GameObject model;
    public ItemType type;
    public Slots slot;
}