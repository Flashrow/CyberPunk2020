using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCharacterItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    // UI fields
    public Text text;
    public Text amount;
    public Image image;

    [SerializeField]
    private Slots slot;

    // DetailsWindow Prefab
    InventoryItemDetailsWindow detailsWindowPreFab;
    public InventoryItemDetailsWindow detailsWindowPreFabTemp;

    bool isPointerOver = false;
    Vector3 position;

    void Start () {

    }

    public void OnPointerClick (PointerEventData pointerEventData) {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
            PlayerManager.Instance.HeroScript.inventory.MoveItemToInventory (slot);
        if (pointerEventData.button == PointerEventData.InputButton.Right)
            PlayerManager.Instance.HeroScript.inventory.DropItem (slot);
    }

    public void OnPointerEnter (PointerEventData eventData) {
        isPointerOver = true;
        LeanTween.scale (this.gameObject, new Vector3 (1.1f, 1.1f, 1), 0.03f);
        StartCoroutine (CreateDetailsWindow (eventData));
    }

    IEnumerator CreateDetailsWindow (PointerEventData eventData) {
        yield return new WaitForSeconds (.5f);
        if (isPointerOver && !detailsWindowPreFab && !PlayerManager.Instance.HeroScript.inventory.IsSlotEmpty (slot)) {
            detailsWindowPreFab = (InventoryItemDetailsWindow) Instantiate (detailsWindowPreFabTemp, transform);
            RectTransform rt = (RectTransform) detailsWindowPreFab.transform;
            float width = rt.rect.width;
            float height = rt.rect.height;
            detailsWindowPreFab.transform.position = eventData.position + new Vector2 (width / 2, -height / 2);
            detailsWindowPreFab.Prime (PlayerManager.Instance.HeroScript.inventory.slots[slot]);
        }
    }

    public void OnPointerExit (PointerEventData eventData) {
        isPointerOver = false;
        LeanTween.scale (this.gameObject, new Vector3 (1, 1, 1), 0.03f);
        if (detailsWindowPreFab) DestroyImmediate (detailsWindowPreFab.gameObject);
    }

    void LoadData () {
        try {
            text.text = PlayerManager.Instance.HeroScript.inventory.slots[slot].itemId;
            amount.text = $"{PlayerManager.Instance.HeroScript.inventory.slots[slot].number}";
            image.sprite = PlayerManager.Instance.HeroScript.inventory.slots[slot].data.sprite;
        } catch {
            text.text = "";
            amount.text = "";
            image.sprite = null;
        }
    }

    private void OnEnable () {
        Inventory.onInventoryChange += LoadData;
    }

    private void OnDisable () {
        Inventory.onInventoryChange -= LoadData;
    }
}