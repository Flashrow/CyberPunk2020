using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour {
    [SerializeField] private GameObject canvas;
    [SerializeField] private string strOpenUIKey;
    [SerializeField] private List<GameObject> subCanvas;
    private bool isOpen = false;
    private int inx = 0;
    // Update is called once per frame
    void Update () {
        if (UIManager.isBlock == false) {
            if (Input.GetKeyDown (strOpenUIKey) && UIManager.isOpen == false) {
                UIManager.UIOpen (ref canvas);
                isOpen = true;
                inx = 0;
            }
            if (isOpen) {
                if (Input.GetKeyDown (KeyCode.N) && inx < subCanvas.Count) {
                    GameObject go = subCanvas[inx];
                    UIManager.UIOpen (ref go);
                    inx++;
                } else if (Input.GetKeyDown (KeyCode.Backspace) && inx > 0) {
                    UIManager.UIReturn ();
                    inx--;
                }
            }
        }
    }
}