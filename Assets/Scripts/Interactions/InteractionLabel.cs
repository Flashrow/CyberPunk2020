using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionLabel : MonoBehaviour
{
    public Text text;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLabel(string value)
    {
        text.text = value;
    }
}
