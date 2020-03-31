using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestListItemDisplay : MonoBehaviour
{
    public Text text;
    public Image image;
    public int amount = 25;
    // Start is called before the first frame update
    void Start()
    {
        text.text = $"x{amount}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prime(Item item)
    {
        this.amount = item.number;
        text.text = $"x{amount}";
        image.sprite = item.data.sprite;
    }
}
