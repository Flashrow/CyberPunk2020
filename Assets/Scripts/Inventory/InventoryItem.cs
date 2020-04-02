using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    // UI fields
    public Text text;
    public Text amount;
    public Image image;
    public Item item = new Item ();

    // DetailsWindow Prefab
    InventoryItemDetailsWindow detailsWindowPreFab;
    public InventoryItemDetailsWindow detailsWindowPreFabTemp;

    bool isPointerOver = false;
    Vector3 position;

    [SerializeField]
    private Inventory inventory;

    void Start () {

    }

    public void OnPointerClick (PointerEventData pointerEventData) {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
            inventory.MoveItemToCharacter (item);
        if (pointerEventData.button == PointerEventData.InputButton.Right)
            inventory.DropItem (item.data.type);
    }

    public void OnPointerEnter (PointerEventData eventData) {
        isPointerOver = true;
        LeanTween.scale (this.gameObject, new Vector3 (1.1f, 1.1f, 1), 0.03f);
        StartCoroutine (CreateDetailsWindow (eventData));
    }

    IEnumerator CreateDetailsWindow (PointerEventData eventData) {
        yield return new WaitForSeconds (.5f);
        if (isPointerOver && !detailsWindowPreFab) {
            detailsWindowPreFab = (InventoryItemDetailsWindow) Instantiate (detailsWindowPreFabTemp, transform);
            RectTransform rt = (RectTransform) detailsWindowPreFab.transform;
            float width = rt.rect.width;
            float height = rt.rect.height;
            detailsWindowPreFab.transform.position = eventData.position + new Vector2 (width / 2, -height / 2);
            detailsWindowPreFab.Prime (item);
        }
    }

    public void OnPointerExit (PointerEventData eventData) {
        isPointerOver = false;
        LeanTween.scale (this.gameObject, new Vector3 (1, 1, 1), 0.03f);
        if (detailsWindowPreFab) DestroyImmediate (detailsWindowPreFab.gameObject);
    }

    public void Prime (Item item) {
        this.item = item;
        try {
            text.text = item.itemId;
        } catch { }
        amount.text = $"{item.number}";
        image.sprite = item.data.sprite;
    }

    private void refresh () {
        if (inventory.HasItem (item.data.type))
            Prime (item);
        else
            DestroyImmediate (gameObject);
    }

    private void OnEnable () {
        Inventory.onInventoryChange += refresh;
    }

    private void OnDisable () {
        Inventory.onInventoryChange -= refresh;
    }
}