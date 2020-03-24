using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsInteractions : MonoBehaviour
{
    bool isActive = false;
    public List<Item> items = new List<Item>();
    public ChestDisplay chestDisplayPrefab;
    private ChestDisplay chestDisplayPrefabUI;
    MoneySystem moneySystem;

    void Awake()
    {
        moneySystem = GameObject.FindObjectOfType<MoneySystem>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Escape)) {
            CloseUI();
        }
        if (isActive && Input.GetKeyDown(KeyCode.Q))
        {
            receiveItems();
            Debug.Log($"Items receiverd, amount: {items.Count}");
            items.Clear();
            CloseUI();
        }
    }

    void receiveItems()
    {
        items.ForEach(delegate(Item item)
        {
            switch (item.type)
            {
                case ItemType.Coins:
                    moneySystem.Add(item.number);
                    break;
            }
        });
    }

    void CloseUI()
    {
        DestroyImmediate(chestDisplayPrefabUI.gameObject);
        isActive = false;
    }

    public void Interact()
    {
        if (isActive) return;
        Debug.Log("Chest Interaction");
        isActive = true;
        foreach (Item item in items)
        {
            System.Random rnd = new System.Random();
            item.number = rnd.Next(item.minNumber, item.maxNumber);
        }
        chestDisplayPrefabUI = (ChestDisplay)Instantiate(chestDisplayPrefab);
        chestDisplayPrefabUI.Prime(items);
    }
}
