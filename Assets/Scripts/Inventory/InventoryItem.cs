using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{
    public Text text;
    public Text amount;
    public Image image;
    public Item item;
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
        try
        {
            text.text = item.itemId;
        }
        catch { }
        amount.text = $"{item.number}";
        image.sprite = item.sprite;
    }
}
