using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    Transform target;
    [SerializeField] bool horizontal = false;
    [SerializeField] bool vertical = false;

    private delegate void Rotate();
    Rotate rotate;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        if (horizontal && !vertical)
        {
            rotate = new Rotate(_horizontal);
        } else if (vertical && !horizontal)
        {
            rotate = new Rotate(_vertical);
        } else Debug.LogError("Invalid type of rotation !!!");
    }

    void _horizontal()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void _vertical()
    {
        transform.LookAt(target, Vector3.up);
    }

    private void Update()
    {
        if (target == null)
            return;

        rotate();
    }
}
