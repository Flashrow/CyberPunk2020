using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interacted {
    [SerializeField]
    private ShopItems shopItems;

    [SerializeField]
    private ShopUI shopUI;
    private ShopUI shopUITemp;

    private bool active = false;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (active && shopUITemp == null)
        {
            _endInteractedEvent.Invoke();
            active = false;
        }
    }

    public override void OnInteract () {
        if (shopUITemp == null) {
            shopUITemp = (ShopUI) Instantiate (shopUI);
            Debug.Log (shopItems.items.Count);
            shopUITemp.Prime (shopItems);
            active = true;
        }
    }
}