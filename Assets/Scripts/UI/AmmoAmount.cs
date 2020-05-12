using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoAmount : MonoBehaviour
{
    static Text text = null;
    void Awake()
    {
        text = GameObject.Find("AmmoAmount").GetComponent<Text>();
    }

    static public void UpdateAmmoUI(string t = "0")
    {
        text.text = t;
    }
}
