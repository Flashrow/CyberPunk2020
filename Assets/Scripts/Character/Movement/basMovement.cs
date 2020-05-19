using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basMovement : MonoBehaviour {
    public float trotSpeed = 10f;
    public float sprintSpeed = 20f;

    public float gravity = 6 * 9.81f;
    public float jumpHeight = 4f;

    public float maximumSlopeAngle = 20f;
    public float slideFriction = 0.7f;

    Vector3 velocity = Vector3.zero;

    public CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    private float stepTimer = 0f;
    private Hero playerScript;
    private enum stepType { walk, trot, sprint }

    // Start is called before the first frame update
    void Start () {
        controller = GetComponent<CharacterController> ();
        playerScript = transform.GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update () {
        if(PlayerManager.Instance.Player.GetComponent<CharacterController>().enabled == true)
        {
            getInput();

            if (controller.isGrounded)
            { 
               if(!slidingDown())
                {
                    checkForJump();
                }
                else
                {
                    wallJump();
                }
            } else
            {
                gravityPhysics();
            }

            if (moveDirection != Vector3.zero)
            {
                setMovingWay();
            }
            else
            {
                playerScript.setMovementState(MovementState.standing);
            }

            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void wallJump()
    {
        
    }

    private void stickToWall()
    {
        
    }

    private bool slidingDown() // must check if is grounded
    {
        float groundAngle = 0f;
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            groundAngle = Vector3.Angle(Vector3.up, hit.normal);
        }

        if(groundAngle > maximumSlopeAngle)
        {
            moveDirection.x += hit.normal.x * slideFriction * (1.0f / (groundAngle / 360.0f));
            moveDirection.z += hit.normal.z * slideFriction * (1.0f / (groundAngle / 360.0f));
            return true;
        }

        return false;
    }

    private void setMovingWay()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (controller.isGrounded)
            {
                stepSound(stepType.sprint);
            }
            playerScript.setMovementState(MovementState.running);
            controller.Move(sprintSpeed * moveDirection * Time.deltaTime);
        }
        else
        {
            if (controller.isGrounded)
            {
                stepSound(stepType.trot);
            }
            playerScript.setMovementState(MovementState.walking);
            controller.Move(trotSpeed * moveDirection * Time.deltaTime);
        }
    }

    private void getInput()
    {
        moveDirection = transform.right * Input.GetAxis("Horizontal") +
                            transform.forward * Input.GetAxis("Vertical");
    }

    private void checkForJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
    }

    private void gravityPhysics()
    {
        velocity.y -= gravity * Time.deltaTime;
    }

    private void stepSound (stepType type) {
        switch (type) {
            case stepType.sprint:
                if (stepTimer > 0.2) {
                    try {
                        AudioManager.instance.playSound ("step");
                    } catch { }
                    stepTimer = 0f;
                } else {
                    stepTimer += Time.deltaTime;
                }
                break;
            case stepType.trot:
                if (stepTimer > 0.4) {
                    try {
                        AudioManager.instance.playSound ("step");
                    } catch { }
                    stepTimer = 0f;
                } else {
                    stepTimer += Time.deltaTime;
                }
                break;
        }

    }
}