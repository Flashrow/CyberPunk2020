using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject gameInterface;
    [SerializeField] private GameObject quickMenu;
    [SerializeField] private GameObject invetory;
    static private GameObject current = null;
    static private bool isOpen = false;
    void Update () {
        if (isOpen == false && Input.GetKeyDown (KeyCode.E)) {
            this.UIOpen (ref invetory);
        }
        if (isOpen == true && Input.GetKeyDown (KeyCode.Escape)) {
            this.UIClose ();
        } else if (isOpen == false && Input.GetKeyDown (KeyCode.Escape)) {
            this.UIOpen (ref quickMenu);
        }
    }
    private void UIOpen (ref GameObject go) {
        if (current != null) return;
        current = go;
        gameInterface.SetActive (false);
        current.SetActive (true);
        isOpen = true;
    }
    private void UIClose () {
        if (current == null) return;
        gameInterface.SetActive (true);
        current.SetActive (false);
        current = null;
        isOpen = false;
    }
    static public void SetUIasOpen () {
        if (current == null)
            isOpen = true;
        else throw new UIException ();
    }
    static public void SetUIasClose () {
        if (current == null)
            isOpen = false;
        else throw new UIException ();
    }
    static public bool IsUIOpen () {
        return isOpen;
    }
    static public void SetCurrentNullable () {
        current = null;
        isOpen = false;
    }
}

public class UIException : System.Exception { }