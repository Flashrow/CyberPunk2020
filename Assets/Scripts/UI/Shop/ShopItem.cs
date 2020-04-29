using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public Text amount;
    public Image image;
    public Item item = new Item ();

    public delegate void OnPointerEnterItem (Item item);
    public static OnPointerEnterItem onPointerEnterItem;
    public delegate void OnShopItemClick (Item item);
    public static OnShopItemClick onShopItemClick;

    [SerializeField]
    private ShopItems items;

    void Start () {

    }

    public void Prime (Item item) {
        this.item = item;
        amount.text = $"{item.number}";

        if (item is Weapon)

            try { Debug.Log("ShopItem: found weapon #" + ((WeaponScriptable)item.data).itemId + "#"); }
            catch
            {
                Debug.Log("ShopItem: found, ale zjebane");
            }

        else
            Debug.Log("ShopItem: weapon not found");


        image.sprite = item.data.sprite;
    }

    public void OnPointerEnter (PointerEventData eventData) {
        LeanTween.scale (this.gameObject, new Vector3 (1.1f, 1.1f, 1), 0.03f);
        try {
            onPointerEnterItem (item);
        } catch { };
    }

    public void OnPointerExit (PointerEventData eventData) {
        LeanTween.scale (this.gameObject, new Vector3 (1, 1, 1), 0.03f);
    }

    public void OnPointerClick (PointerEventData eventData) {
        onShopItemClick (item);
    }

}