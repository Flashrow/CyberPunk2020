using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrowScript : MonoBehaviour
{
    private Transform target = null;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(target != null)
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public void SetTarget(Transform obj)
    {
        target = obj;
        gameObject.SetActive(true);
    }

    public void RemoveTarget()
    {
        target = null;
        gameObject.SetActive(false);
    }
}
