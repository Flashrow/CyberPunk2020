using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionLabel : MonoBehaviour {
    public Text text;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void SetLabel (string value) {
        text.text = value;
    }
}