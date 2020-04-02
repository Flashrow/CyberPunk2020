using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private ShopCharacterItem itemPreFab;
    [SerializeField]
    private ShopItem shopItemPreFab;
    [SerializeField]
    private Transform itemsContainer;
    [SerializeField]
    private Transform shopItemsContainer;

    [SerializeField]
    private Text itemDetailsPrice;
    [SerializeField]
    private Text itemDetailsName;
    
    private ShopItems shopItems;
    // Use this for initialization
    private void OnEnable()
    {
        Inventory.onAddItemInventory += DisplayItem;
        ShopItem.onPointerEnterItem += FillDetailsLabel;
    }

    private void OnDisable()
    {
        Inventory.onAddItemInventory -= DisplayItem;
        ShopItem.onPointerEnterItem -= FillDetailsLabel;
    }

    void Start()
    {
        foreach (KeyValuePair<ItemType, Item> item in inventory.items)
        {
            DisplayItem(item.Value);
        }
        foreach (KeyValuePair<ItemType, Item> item in shopItems.items)
        {
            DisplayShopItem(item.Value);
        }
    }

    void DisplayItem(Item item)
    {
        ShopCharacterItem display = (ShopCharacterItem)Instantiate(itemPreFab);
        display.transform.SetParent(itemsContainer); 
        display.transform.localScale = new Vector3(1, 1, 1);
        display.Prime(item);
    }

    void DisplayShopItem(Item item)
    {
        ShopItem display = (ShopItem)Instantiate(shopItemPreFab);
        display.transform.SetParent(shopItemsContainer);
        display.transform.localScale = new Vector3(1, 1, 1);
        display.Prime(item);
    }

    void FillDetailsLabel(Item item)
    {
        itemDetailsPrice.text = item.cost.ToString();
        itemDetailsName.text = item.itemId;
    }

    // Update is called once per frame
    void Update()
    {
        OnEscPress();
    }

    public void Prime(ShopItems items)
    {
        Debug.Log(items.items.Count);
        shopItems = items;
    }

    void OnEscPress()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) DestroyMe();
    }

    public void DestroyMe()
    {
        DestroyImmediate(gameObject);
    }
}
