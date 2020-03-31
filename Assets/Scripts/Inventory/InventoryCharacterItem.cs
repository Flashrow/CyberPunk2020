using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryCharacterItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // UI fields
    public Text text;
    public Text amount;
    public Image image;

    [SerializeField]
    private Slots slot;

    [SerializeField]
    private Inventory inventory;
    // DetailsWindow Prefab
    InventoryItemDetailsWindow detailsWindowPreFab;
    public InventoryItemDetailsWindow detailsWindowPreFabTemp;

    bool isPointerOver = false;
    Vector3 position;

    void Start()
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
            inventory.MoveItemToInventory(slot);
        if (pointerEventData.button == PointerEventData.InputButton.Right)
            inventory.DropItem(slot);        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        LeanTween.scale(this.gameObject, new Vector3(1.1f, 1.1f, 1), 0.03f);
        StartCoroutine(CreateDetailsWindow(eventData));
    }

    IEnumerator CreateDetailsWindow(PointerEventData eventData)
    {
        yield return new WaitForSeconds(.5f);
        if (isPointerOver && !detailsWindowPreFab && !inventory.IsSlotEmpty(slot))
        {
            detailsWindowPreFab = (InventoryItemDetailsWindow)Instantiate(detailsWindowPreFabTemp, transform);
            RectTransform rt = (RectTransform)detailsWindowPreFab.transform;
            float width = rt.rect.width;
            float height = rt.rect.height;
            detailsWindowPreFab.transform.position = eventData.position + new Vector2(width / 2, -height / 2);
            detailsWindowPreFab.Prime(inventory.slots[slot]);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), 0.03f);
        if (detailsWindowPreFab) DestroyImmediate(detailsWindowPreFab.gameObject);
    }

    void LoadData()
    {
        try
        {
            text.text = inventory.slots[slot].itemId;
            amount.text = $"{inventory.slots[slot].number}";
            image.sprite = inventory.slots[slot].data.sprite;
        }
        catch
        {
            text.text = "";
            amount.text = "";
            image.sprite = null;
        }
    }

    private void OnEnable()
    {
        Inventory.onInventoryChange += LoadData;       
    }

    private void OnDisable()
    {
        Inventory.onInventoryChange -= LoadData;
    }
}
