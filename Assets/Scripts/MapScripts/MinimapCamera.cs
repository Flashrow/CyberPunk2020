using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        Transform player = PlayerManager.Instance.Player.transform;
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
