using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestDisplay : MonoBehaviour {
    public Transform targetTransform;
    public ChestListItemDisplay itemListPreFabs;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void Prime (List<Item> items) {
        System.Random rnd = new System.Random ();
        foreach (Item item in items) {
            ChestListItemDisplay display = (ChestListItemDisplay) Instantiate (itemListPreFabs);
            display.transform.SetParent (targetTransform, false);
            display.Prime (item);
        }
    }
}