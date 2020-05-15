using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
    float mouseX = 0f;
    float mouseY = 0f;
    float rotationX = 0f;

    public bool cursorVisibility = false;
    public float rotationSpeed = 100f;

    // Start is called before the first frame update
    void Start () {
        Cursor.visible = cursorVisibility;
    }

    // Update is called once per frame
    void FixedUpdate () {

        mouseX = rotationSpeed * Input.GetAxis ("Mouse X") * Time.deltaTime;
        mouseY = rotationSpeed * Input.GetAxis ("Mouse Y") * Time.deltaTime;

        rotationX -= mouseY;

        rotationX = Mathf.Clamp (rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler (rotationX, 0f, 0f);
        PlayerManager.Instance.Player.transform.Rotate (Vector3.up * mouseX);
    }
    
}