﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text amount;
    public Image image;
    public Item item = new Item();

    public delegate void OnPointerEnterItem(Item item);
    public static OnPointerEnterItem onPointerEnterItem;

    [SerializeField]
    private Inventory inventory;

    void Start()
    {

    }

    public void Prime(Item item)
    {
        this.item = item;
        amount.text = $"{item.number}";
        image.sprite = item.data.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1.1f, 1.1f, 1), 0.03f);
        try
        {
            onPointerEnterItem(item);
        }
        catch { };
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), 0.03f);
    }
}
