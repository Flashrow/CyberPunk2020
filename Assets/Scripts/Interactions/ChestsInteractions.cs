using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsInteractions : MonoBehaviour
{
    bool isActive = false;
    List<Item> items = new List<Item>();
    public ChestDisplay chestDisplayPrefab;
    private ChestDisplay chestDisplayPrefabUI;
    MoneySystem moneySystem;

    void Awake()
    {
        moneySystem = GameObject.FindObjectOfType<MoneySystem>();
    }

    void Start()
    {
        this.items.Add(new Ammo());     
        foreach(Item item in items)
        {
            item.number = Random.Range(item.minNumber, item.maxNumber + 1);
        }
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
            if(item is Ammo)
            {
                moneySystem.Add(item.number);
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
        chestDisplayPrefabUI = (ChestDisplay)Instantiate(chestDisplayPrefab);
        chestDisplayPrefabUI.Prime(items);
    }
}
