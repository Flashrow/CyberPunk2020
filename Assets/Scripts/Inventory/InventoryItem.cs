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

    bool isPointerOver = false;

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
        isPointerOver = true;
        LeanTween.scale(this.gameObject, new Vector3(1.1f, 1.1f, 1), 0.03f);
        StartCoroutine(CreateDetailsWindow(eventData));
    }

    IEnumerator CreateDetailsWindow(PointerEventData eventData)
    {
        yield return new WaitForSeconds(.5f);
        if (isPointerOver && !detailsWindowPreFab)
        {
            detailsWindowPreFab = (InventoryItemDetailsWindow)Instantiate(detailsWindowPreFabTemp, transform);
            RectTransform rt = (RectTransform)detailsWindowPreFab.transform;
            float width = rt.rect.width;
            float height = rt.rect.height;
            detailsWindowPreFab.transform.position = eventData.position + new Vector2(width/2, -height/2);
            detailsWindowPreFab.Prime(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), 0.03f);
        if(detailsWindowPreFab) DestroyImmediate(detailsWindowPreFab.gameObject);
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
