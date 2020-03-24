using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtTargetWithoutAnimate : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos =  transform.position - target.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }
}
