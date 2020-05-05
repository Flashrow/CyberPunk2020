using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMarkerColor : MonoBehaviour
{
    public Color color = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Renderer>().material.SetColor("_Color", color); 
        transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}
