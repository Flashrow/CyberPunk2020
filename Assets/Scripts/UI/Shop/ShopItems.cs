using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ListItem
{
    public int number;
    public int cost;
    public ItemType type;
    ListItem(int n,int c ,ItemType t)
    {
        number = n;
        type = t;
        cost = c;
    }
}

[CreateAssetMenu(fileName = "ShopItemsData", menuName = "ShopItems", order = 2)]
public class ShopItems : ScriptableObject
{
    [SerializeField]
    private List<ListItem> list = new List<ListItem>();

    public Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item>();
    // Use this for initialization
    void OnEnable()
    {
        Debug.Log("elo");
        foreach(ListItem item in list)
        {
            Debug.Log("ShopItems: " + item.type);
            items.Add(item.type, Item.CreateItemObjectByType(item.type));
            items[item.type].number = item.number;
            items[item.type].cost = item.cost;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
