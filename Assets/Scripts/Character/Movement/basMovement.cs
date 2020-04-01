using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basMovement : MonoBehaviour
{
    public float trotSpeed = 10f;
    public float sprintSpeed = 20f;

    public float gravity = 6 * 9.81f;
    public float jumpHeight = 4f;

    Vector3 velocity = Vector3.zero;

    public CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    private float stepTimer = 0f;

    private enum stepType { walk, trot, sprint };

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = transform.right * Input.GetAxis("Horizontal") +
            transform.forward * Input.GetAxis("Vertical");

        if (controller.isGrounded
            && velocity.y <= 0)
        {
            velocity.y = -2f;
        }

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (controller.isGrounded)
                {
                    stepSound(stepType.sprint);
                }

                controller.Move(sprintSpeed * moveDirection * Time.deltaTime);
            }
            else
            {
                if (controller.isGrounded)
                {
                    stepSound(stepType.trot);
                }

                controller.Move(trotSpeed * moveDirection * Time.deltaTime);
            }
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void stepSound(stepType type)
    {
        switch (type)
        {
            case stepType.sprint:
                if (stepTimer > 0.2)
                {
                    try
                    {
                        AudioManager.instance.playSound("step");
                    }
                    catch { }
                    stepTimer = 0f;
                }
                else
                {
                    stepTimer += Time.deltaTime;
                }
                break;
            case stepType.trot:
                if (stepTimer > 0.4)
                {
                    try
                    {
                        AudioManager.instance.playSound("step");
                    }
                    catch { }
                    stepTimer = 0f;
                }
                else
                {
                    stepTimer += Time.deltaTime;
                }
                break;
        }

    }
}