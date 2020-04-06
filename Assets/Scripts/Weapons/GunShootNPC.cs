using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootNPC : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 20f;
    }
}
