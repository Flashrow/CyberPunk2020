using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProbability {
    public Item item;
    public int probability;
    public ItemProbability(Item item, int probability)
    {
        this.item = item;
        this.probability = probability;
    }
};

public class ChestsInteractions : MonoBehaviour
{
    bool isActive = false;
    List<Item> items = new List<Item>();
    public ChestDisplay chestDisplayPrefab;
    private ChestDisplay chestDisplayPrefabUI;
    public List<ItemProbability> itemProbability = new List<ItemProbability>();
    MoneySystem moneySystem;
    Inventory inventory;

    void Awake()
    {
        moneySystem = GameObject.FindObjectOfType<MoneySystem>();
    }

    void LoadItemsDictionary()
    {
        itemProbability.Add(new ItemProbability(new Ammo(), 70 ));
        itemProbability.Add(new ItemProbability(new Coins(), 100 ));
        itemProbability.Add(new ItemProbability(new Tools(), 20 ));
        itemProbability.Add(new ItemProbability(new Phone(), 10 ));
    }

    void RandomItems()
    {
        LoadItemsDictionary();        
        foreach(ItemProbability item in itemProbability)
        {
            if(Random.Range(0, 101) < item.probability) items.Add(item.item);
        }        
    }

    void Start()
    {
        inventory = GameObject.FindObjectOfType<Hero>().inventory;
        RandomItems();
        foreach (Item item in items)
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
            if(item is Coins)
            {
                moneySystem.Add(item.number);
            } else
            {
                inventory.AddItem(item);
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
