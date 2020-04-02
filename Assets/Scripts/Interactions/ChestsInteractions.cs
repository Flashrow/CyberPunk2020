using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProbability {
    public Item item;
    public int probability;
    public ItemProbability (Item item, int probability) {
        this.item = item;
        this.probability = probability;
    }
};

public class ChestsInteractions : Interacted {
    bool isActive = false;
    List<Item> items = new List<Item> ();
    public ChestDisplay chestDisplayPrefab;
    private ChestDisplay chestDisplayPrefabUI;
    public List<ItemProbability> itemProbability = new List<ItemProbability> ();

    [SerializeField]
    private MoneySystem moneySystem;
    [SerializeField]
    private Inventory inventory;

    void LoadItemsDictionary () {
        itemProbability.Add (new ItemProbability (new Ammo (), 100));
        itemProbability.Add (new ItemProbability (new Coins (), 100));
        itemProbability.Add (new ItemProbability (new Tools (), 100));
        itemProbability.Add (new ItemProbability (new Phone (), 100));
    }

    void RandomItems () {
        LoadItemsDictionary ();
        foreach (ItemProbability item in itemProbability) {
            if (Random.Range (0, 101) < item.probability) items.Add (item.item);
        }
    }

    void Start () {
        RandomItems ();
        foreach (Item item in items) {
            item.number = Random.Range (item.data.minNumber, item.data.maxNumber + 1);
        }
    }

    void Update () {
        if (isActive && Input.GetKeyDown (KeyCode.Escape)) {
            CloseUI ();
        }
        if (isActive && Input.GetKeyDown (KeyCode.Q)) {
            receiveItems ();
            Debug.Log ($"Items receiverd, amount: {items.Count}");
            items.Clear ();
            CloseUI ();
        }
    }

    void receiveItems () {
        items.ForEach (delegate (Item item) {
            if (item is Coins) {
                moneySystem.Add (item.number);
            } else {
                inventory.AddItem (item);
            }
        });
    }

    void CloseUI () {
        DestroyImmediate (chestDisplayPrefabUI.gameObject);
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