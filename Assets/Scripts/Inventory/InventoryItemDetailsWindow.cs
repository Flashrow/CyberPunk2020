using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItemDetailsWindow : MonoBehaviour
{
    public Item item;
    public Text number;
    public Text nameLabel;
    public Text cost;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Prime(Item item)
    {
        this.item = item;
        number.text = $"{item.number}";
        cost.text = $"{item.cost}";
        nameLabel.text = $"{item.itemId}";
    }
}
