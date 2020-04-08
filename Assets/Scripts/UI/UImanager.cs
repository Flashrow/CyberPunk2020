using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject gameInterface;
    [SerializeField] private GameObject quickMenu;
    [SerializeField] private GameObject invetory;
    private GameObject current = null;
    static public bool isOpen { get; set; }
    void Awake () {
        isOpen = false;
    }
    void Update () {
        if (isOpen == false && Input.GetKeyDown (KeyCode.E)) {
            UIOpen (ref invetory);
        }
        if (isOpen == true && Input.GetKeyDown (KeyCode.Escape)) {
            UIClose ();
        } else if (isOpen == false && Input.GetKeyDown (KeyCode.Escape)) {
            UIOpen (ref quickMenu);
        }
    }
    public void UIOpen (ref GameObject go) {
        if (current != null) return;
        current = go;
        gameInterface.SetActive (false);
        current.SetActive (true);
        isOpen = true;
    }
    public void UIClose () {
        if (current == null) return;
        gameInterface.SetActive (true);
        current.SetActive (false);
        current = null;
        isOpen = false;
    }
}