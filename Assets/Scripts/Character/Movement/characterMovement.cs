using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public Transform cameraView;

    public float runVelocity = 10;
    public float turnSpeed;

    private Quaternion targetRotation;
    private Vector2 input;
    private Vector3 movingDirection;
    private float angle;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        if (movingDirection.x == 0f && movingDirection.z == 0f) return;
        prepareMovingVector();
        calculateDirection();
        
        move();
    }

    private void FixedUpdate()
    {
        
    }

    private void getInput()
    {
        movingDirection.x =  Input.GetAxisRaw("Horizontal");
        movingDirection.z =  Input.GetAxisRaw("Vertical");
    }

    private void prepareMovingVector()
    {
        float angl = Vector3.Angle(Vector3.forward, movingDirection);
        movingDirection.x += transform.forward.x;
        movingDirection.z += transform.forward.z;
    }

    private void calculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle *= Mathf.Rad2Deg;
        angle += cameraView.eulerAngles.y;
    }

    private void rotate()
    {
        targetRotation = Quaternion.Euler(0, angle/2, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
    }

    private void move()
    {
        Debug.Log("characterMovement: moving direction:" + movingDirection);
        transform.position += movingDirection * runVelocity * Time.deltaTime;
    }
}

