using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtTarget : MonoBehaviour {
    public Transform target;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (target == null) {
            target = GameObject.FindObjectOfType<Hero> ().transform;
        }
        Vector3 relativePos = transform.position - target.position;
        Quaternion rotation = Quaternion.LookRotation (relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp (current, rotation, 2 * Time.deltaTime);
    }
}