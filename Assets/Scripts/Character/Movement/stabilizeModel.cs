using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stabilizeModel : MonoBehaviour {
    public Transform playerTransform;
    public Transform modelTransform;

    // Update is called once per frame
    void Update () {
        modelTransform.position = new Vector3 (playerTransform.position.x,
            playerTransform.position.y - 1.19f,
            playerTransform.position.z);

        modelTransform.rotation = playerTransform.rotation;
    }
}