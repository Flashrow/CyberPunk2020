using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public Text amount;
    public Image image;
    public Item item = new Item();

    InventoryItemDetailsWindow detailsWindowPreFab;
    public InventoryItemDetailsWindow detailsWindowPreFabTemp;

    // Use this for initialization
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
    }

    bool IsMouseOver()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailsWindowPreFab = (InventoryItemDetailsWindow)Instantiate(detailsWindowPreFabTemp, transform);
        detailsWindowPreFab.transform.position = eventData.position + new Vector2(20, -20);
        detailsWindowPreFab.Prime(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyImmediate(detailsWindowPreFab.gameObject);
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
