using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemProbability {
    public Item item;
    [Range (0, 100)]
    public int probability;
    public int cost;
    public int number;
    public ItemType type;
    public ItemProbability (Item item, int probability) {
        this.item = item;
        this.probability = probability;
    }
};

public class ChestsInteractions : Interacted {
    List<Item> items = new List<Item> ();
    public ChestDisplay chestDisplayPrefab;
    private ChestDisplay chestDisplayPrefabUI;

    [SerializeField]
    private MoneySystem moneySystem;

    [SerializeField]
    public List<ItemProbability> itemProbability = new List<ItemProbability> ();

    void LoadItemsDictionary () {
        foreach (ItemProbability item in itemProbability) {
            item.item = Item.CreateItemObjectByType (item.type);
            item.item.cost = item.cost;
            item.item.number = item.number;
        }
    }

    void RandomItems () {
        LoadItemsDictionary ();
        foreach (ItemProbability item in itemProbability) {
            if (Random.Range (0, 100) < item.probability) items.Add (item.item);
        }
    }

    void Start () {
        RandomItems ();
        foreach (Item item in items) {
            //item.number = Random.Range (item.data.minNumber, item.data.maxNumber + 1);
        }
    }

    void Update () {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseUI();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                receiveItems();
                if(items.Count > 0)
                    UImanager.Alert($"Items receiverd to inventory, amount: {items.Count}");
                else UImanager.Alert($"Chest is empty");
                items.Clear();
                CloseUI();
            }
        }
    }

    void receiveItems () {
        items.ForEach (delegate (Item item) {
            if (item is Coins) {
                moneySystem.Add (item.number);
            } else {
                EventListener.instance.Inventory.Invoke(new ItemData {
                    item = item,
                    eventType = ItemEventType.TAKEN
                });
                PlayerManager.Instance.HeroScript.inventory.AddItem (item);
            }
        });
    }

    void CloseUI () {
        DestroyImmediate (chestDisplayPrefabUI.gameObject);
        Input.ResetInputAxes();
        _endInteractedEvent.Invoke();
        isActive = false;
    }

    public override void OnInteract () {
        if (isActive) return;
        isActive = true;
        chestDisplayPrefabUI = (ChestDisplay) Instantiate (chestDisplayPrefab);
        chestDisplayPrefabUI.Prime (items);
    }

    public override void OnCancelIntegration () {
        CloseUI ();
    }
}