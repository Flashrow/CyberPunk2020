using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
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

    [SerializeField]
    private ShopSliderWindow shopSliderWindowPreFab;
    ShopSliderWindow shopSliderWindowPreFabTemp;

    private ShopItems shopItems;
    // Use this for initialization
    private void OnEnable () {
        Inventory.onAddItemInventory += DisplayItem;
        ShopItem.onPointerEnterItem += FillDetailsLabel;
        ShopItem.onShopItemClick += DisplayBuyWindow;
        ShopCharacterItem.onCharacterItemClick += DisplaySellWindow;
        ShopSliderWindow.onShopItemsChanged += DisplayShopItems;
    }

    private void OnDisable () {
        Inventory.onAddItemInventory -= DisplayItem;
        ShopItem.onPointerEnterItem -= FillDetailsLabel;
        ShopItem.onShopItemClick -= DisplayBuyWindow;
        ShopCharacterItem.onCharacterItemClick -= DisplaySellWindow;
        ShopSliderWindow.onShopItemsChanged -= DisplayShopItems;
    }

    void Start () {
        foreach (KeyValuePair<ItemType, Item> item in PlayerManager.Instance.HeroScript.inventory.items) {
            DisplayItem (item.Value);
        }
        DisplayShopItems ();
    }

    void DisplayItem (Item item) {
        ShopCharacterItem display = (ShopCharacterItem) Instantiate (itemPreFab);
        display.transform.SetParent (itemsContainer);
        display.transform.localScale = new Vector3 (1, 1, 1);
        display.Prime (item);
    }

    void DisplayShopItems () {
        foreach (Transform child in shopItemsContainer) {
            GameObject.Destroy (child.gameObject);
        }
        foreach (KeyValuePair<ItemType, Item> item in shopItems.items) {
            DisplayShopItem (item.Value);
        }
    }

    void DisplayShopItem (Item item) {
        ShopItem display = (ShopItem) Instantiate (shopItemPreFab);
        display.transform.SetParent (shopItemsContainer);
        display.transform.localScale = new Vector3 (1, 1, 1);
        display.Prime (item);
    }

    void DisplayBuyWindow (Item item) {
        if (shopSliderWindowPreFabTemp == null) {
            shopSliderWindowPreFabTemp = (ShopSliderWindow) Instantiate (shopSliderWindowPreFab, transform);
            shopSliderWindowPreFabTemp.Prime (item, "BUY", shopItems);
        }
    }

    void DisplaySellWindow (Item item) {
        if (shopSliderWindowPreFabTemp == null) {
            shopSliderWindowPreFabTemp = (ShopSliderWindow) Instantiate (shopSliderWindowPreFab, transform);
            shopSliderWindowPreFabTemp.Prime (item, "SELL", shopItems);
        }
    }

    void FillDetailsLabel (Item item) {
        itemDetailsPrice.text = item.cost.ToString ();
        itemDetailsName.text = item.itemId;
    }

    // Update is called once per frame
    void Update () {
        OnEscPress ();
    }

    public void Prime (ShopItems items) {
        Debug.Log (items.items.Count);
        shopItems = items;
    }

    void OnEscPress () {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            DestroyMe();
        }
    }

    public void DestroyMe () {
        DestroyImmediate (gameObject);
    }
}