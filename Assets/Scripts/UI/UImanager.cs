using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {
    bool isInventoryActive = false;
    bool isQuickMenuActive = false;
    static bool state;
    public GameObject QuickMenu;
    public GameObject inventoryUI;
    public GameObject gameIntervaceUI;
    void Awake () {
        inventoryUI.SetActive (false);
        QuickMenu.SetActive (false);
    }
    void Update () {
        if (isInventoryActive == false && Input.GetKeyDown (KeyCode.E))
            OpenUI (ref inventoryUI, ref isInventoryActive);
        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (isInventoryActive == true)
                CloseUI (ref inventoryUI, ref isInventoryActive);
            else if (isQuickMenuActive == false)
                OpenUI (ref QuickMenu, ref isQuickMenuActive);
            else CloseUI (ref QuickMenu, ref isQuickMenuActive);
        }
    }

    public void OpenUI (ref GameObject go, ref bool type) {
        gameIntervaceUI.SetActive (false);
        go.SetActive (true);
        type = true;
        state = true;
    }
    public void CloseUI (ref GameObject go, ref bool type) {
        gameIntervaceUI.SetActive (true);
        go.SetActive (false);
        type = false;
        state = false;
    }
    static public bool GetUIState () {
        return state;
    }
}