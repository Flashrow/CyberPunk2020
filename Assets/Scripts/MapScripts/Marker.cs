using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Marker : MonoBehaviour
{
    [SerializeField] GameObject marker = null;

    private void Start()
    {
        if (marker == null) throw null;
    }

    public GameObject Get()
    {
        return marker;
    }

    public void SetNew(GameObject marker)
    {
        this.marker = marker;
    }
}
