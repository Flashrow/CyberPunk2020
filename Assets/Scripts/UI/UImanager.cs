using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    static private GameObject gameInterface;
    [SerializeField] private GameObject quickMenu;
    static private List<GameObject> elements = new List<GameObject> ();
    static public bool isOpen { get; private set; }
    static public bool isBlock { get; private set; }
    void Awake () {
        gameInterface = GameObject.Find ("GameInterface");
        isOpen = false;
        isBlock = false;
    }
    void Update () {
        if (isBlock == false) {
            if (isOpen == true && Input.GetKeyDown (KeyCode.Escape)) {
                UIPermentClose ();
            } else if (isOpen == false && Input.GetKeyDown (KeyCode.Escape)) {
                UIOpen (ref quickMenu);
            }
        }
    }
    static public void UIOpen (ref GameObject go) {
        if (elements.Count == 0) {
            gameInterface.SetActive (false);
            isOpen = true;
        } else {
            elements[elements.Count - 1].SetActive (false);
        }
        elements.Add (go);
        elements[elements.Count - 1].SetActive (true);
    }
    static public void UIReturn () {
        elements[elements.Count - 1].SetActive (false);
        elements.RemoveAt (elements.Count - 1);
        if (elements.Count == 0) {
            gameInterface.SetActive (true);
            isOpen = false;
        } else {
            elements[elements.Count - 1].SetActive (true);
        }
    }
    static public void UIPermentClose () {
        foreach (var item in elements)
            item.SetActive (false);
        elements.Clear ();
        gameInterface.SetActive (true);
        isOpen = false;
    }
    static public void UIBlock () {
        isBlock = true;
    }
    static public void UIUnlock () {
        isBlock = false;
    }
}

public class UIException : System.Exception { }