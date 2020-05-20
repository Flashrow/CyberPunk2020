using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
    float mouseX = 0f;
    float mouseY = 0f;
    float rotationX = 0f;

    private float sinusTime = 0f;
    private Vector3 startCameraPosition;

    public bool cursorVisibility = true;
    public float rotationSpeed = 200f;
    public float bobbingRange = 0.07f;
    public float bobbingFrequency = 15f;
    public Hero playerScript;
    
    // Start is called before the first frame update
    void Start () 
    {
        Cursor.visible = cursorVisibility;
        playerScript = transform.parent.GetComponent<Hero>();
        startCameraPosition = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        mouseX = rotationSpeed * Input.GetAxis ("Mouse X") * Time.deltaTime;
        mouseY = rotationSpeed * Input.GetAxis ("Mouse Y") * Time.deltaTime;

        rotationX -= mouseY;

        rotationX = Mathf.Clamp (rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler (rotationX, 0f, 0f);
        PlayerManager.Instance.Player.transform.Rotate (Vector3.up * mouseX);

        if(playerScript.getMovementState() == MovementState.running)
        {
            cameraBobbing();
        }
        
    }
    
    public void resetCameraPosition()
    {
        transform.position = startCameraPosition;
    }
    void cameraBobbing()
    {
        Vector3 pos = startCameraPosition;
        pos.y += Mathf.Sin(sinusTime * bobbingFrequency) * bobbingRange;
        Debug.Log("cameraMovement: bobbing position: " + pos + " sinus: " + Mathf.Sin(sinusTime) * bobbingRange);
        transform.localPosition= pos;
        sinusTime += Time.deltaTime;
    }
}