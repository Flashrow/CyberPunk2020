using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
    float mouseX = 0f;
    float mouseY = 0f;
    float rotationX = 0f;

    public float rotationSpeed = 100f;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        mouseX = rotationSpeed * Input.GetAxis ("Mouse X") * Time.deltaTime;
        mouseY = rotationSpeed * Input.GetAxis ("Mouse Y") * Time.deltaTime;

        rotationX -= mouseY;

        rotationX = Mathf.Clamp (rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler (rotationX, 0f, 0f);
        playerTransform.Rotate (Vector3.up * mouseX);
    }
}