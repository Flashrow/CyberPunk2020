using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopSliderWindow : MonoBehaviour
{
    Item item;
    string mode;

    int sliderValue = 1;
    int finalyPrice = 0;

    [SerializeField]
    Inventory inventory;
    [SerializeField]
    MoneySystem money;
    ShopItems shopItems;

    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text minValue;
    [SerializeField]
    private Text maxValue;
    [SerializeField]
    private Text cost;
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Slider slider;

    public delegate void ShopItemsChanged();
    public static ShopItemsChanged onShopItemsChanged;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Prime(Item item, string mode, ShopItems items)
    {
        this.item = item;
        this.mode = mode;
        shopItems = items;
        itemName.text = item.data.itemId;
        minValue.text = "1";
        maxValue.text = item.number.ToString();
        cost.text = item.cost.ToString();
        buttonText.text = mode;
        finalyPrice = item.cost;
        slider.maxValue = (int)item.number;
        if(mode == "BUY" && money.money - (item.number*item.cost) < 0)
        {
            slider.maxValue = (int)(money.money / item.cost);
            maxValue.text = item.number.ToString();
        }
        slider.minValue = 1;
    }

    public void OnSliderValueChange()
    {
        sliderValue = (int)slider.value;
        finalyPrice = sliderValue * item.cost;
        cost.text = finalyPrice.ToString();
    }

    public void AcceptButtonClicked()
    {
        if(mode == "BUY")
        {
            money.Buy(finalyPrice);
            shopItems.items[item.data.type].number -= sliderValue;
            Item newItem = item.CreateInstance();
            newItem.number = sliderValue;
            newItem.cost = item.cost;
            newItem.itemId = item.itemId;
            inventory.AddItem(newItem);            
        }
        if (mode == "SELL")
        {
            money.Add(finalyPrice);
            if (!shopItems.items.ContainsKey(item.data.type))
            {
                Item newItem = item.CreateInstance();
                newItem.number = sliderValue;
                shopItems.items.Add(item.data.type, newItem);
            } else
            {
                shopItems.items[item.data.type].number += sliderValue;
            }
            inventory.DecreaseItem(item.data.type, sliderValue);
        }
        try
        {
            onShopItemsChanged();
        }
        catch { }
        DestroyMe();
    }

    public void DestroyMe()
    {
        DestroyImmediate(gameObject);
    }

}
