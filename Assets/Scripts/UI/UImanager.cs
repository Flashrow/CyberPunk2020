using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour {
    static private GameObject gameInterface;
    [SerializeField] private GameObject quickMenuCanvas;
    static private List<GameObject> elements = new List<GameObject> ();
    static public bool isOpen { get; set; }
    static public bool isBlock { get; private set; }
    static public bool alertDisable { get; set; } = false;
    static private Canvas alert;
    void Awake () {
        gameInterface = GameObject.Find ("GameInterface");
        alert = Resources.Load<Canvas>("PreFabs/UI/Alert");
        isOpen = false;
        isBlock = false;
    }
    void Update () {
        if (isBlock == false) {
            if (isOpen == true && Input.GetKeyDown (KeyCode.Escape)) {
                UIPermentClose ();
            }
            else if (isOpen == false && Input.GetKeyDown (KeyCode.Escape)) {
                UIOpen (ref quickMenuCanvas);
            }
        }
        if (isOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    static public void Alert(string text, float time = 2.5f)
    {
        if (isBlock == false && alertDisable == false)
        {
            var obj = GameObject.Instantiate(alert).transform;
            obj.SetParent(GameObject.Find("UI").transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
            GameObject.Destroy(obj.gameObject, time);
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
        UImanager.alertDisable = false;
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