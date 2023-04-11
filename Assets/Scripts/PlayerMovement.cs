using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController cc;

    private float speed = 9.0f;

    private float gravity = -9.81f;
    private float yVelocity = 0f;
    private float yVelocityWhenGrounded = -4f;

    private float jumpHeight = 3.0f;
    private float jumpTime = 0.5f;
    private float initialJumpVelocity;

    private float jumpsAvailable = 0;
    private float jumpsMax = 2;

    [SerializeField] private GameObject model;          // a reference to the model (inside the Player gameObject)
    //private float rotateToFaceMovementSpeed = 5f;       // the speed to rotate our model towards the movement vector

    [SerializeField] private Camera cam;                // a reference to the main camera
    //private float rotateToFaceAwayFromCameraSpeed = 5f; // the speed to rotate our Player to align with the camera view.

    // Start is called before the first frame update
    void Start()
    {
        float timeToApex = jumpTime / 2.0f;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    // Update is called once per frame
    void Update()
    {
        // determine XZ movement vector
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizInput, 0, vertInput);

        // ensure diagonal movement doesn't exceed horiz/vert movement speed
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        // set the animator's velocity parameter based on our XZ movement

        // convert from local to global coordinates


        movement *= speed;

        yVelocity += gravity * Time.deltaTime;

        // if we are on the ground and we were falling
        if (cc.isGrounded && yVelocity < 0.0)
        {
            yVelocity = yVelocityWhenGrounded;
            jumpsAvailable = jumpsMax;
        }

        if (Input.GetButtonDown("Jump") && jumpsAvailable > 0)
        {
            yVelocity = initialJumpVelocity;
            jumpsAvailable--;
            Debug.Log("jump!");
            //anim.SetTrigger("jump");
        }

        movement.y = yVelocity;

        movement *= Time.deltaTime;

        cc.Move(movement);
    }
}
