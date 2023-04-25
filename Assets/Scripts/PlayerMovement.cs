using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cc;

    private float speed = 9.0f;         // XZ movement speed

    private float gravity = -9.81f;
    private float yVelocity = 0f;
    private float yVelocityWhenGrounded = -4f;

    private float jumpHeight = 3.5f;
    private float jumpTime = 0.5f;
    private float initialJumpVelocity;

    private float jumpsAvailable = 0;
    private float jumpsMax = 2;

    //private int mouse0ClickCount = 0;

    [SerializeField] private Animator anim;             // the model's animator component
    [SerializeField] private GameObject model;          // a reference to the model (inside the Player gameObject)
    
    private float rotateToFaceMovementSpeed = 5f;       // the speed to rotate our model towards the movement vector

    [SerializeField] private Camera cam;                // a reference to the main camera
    private float rotateToFaceAwayFromCameraSpeed = 5f; // the speed to rotate our Player to align with the camera view.

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
        anim.SetFloat("velocity", movement.magnitude);

        // convert from local to global coordinates
        movement = transform.TransformDirection(movement);

        if (movement.magnitude > 0)
        {
            RotateModelToFaceMovement(movement);
            RotatePlayerToFaceAwayFromCamera();
        }

        movement *= speed;

        yVelocity += gravity * Time.deltaTime;

        // if we are on the ground and we were falling
        if (cc.isGrounded && yVelocity < 0.0)
        {
            yVelocity = yVelocityWhenGrounded;
            jumpsAvailable = jumpsMax;
        }

        // give upward y velocity if we jump
        if (Input.GetButtonDown("Jump") && jumpsAvailable > 0)
        {
            anim.SetTrigger("jump");
            yVelocity = initialJumpVelocity;
            if(jumpsAvailable == 1) { yVelocity += jumpHeight; }        // Add more height for second(last) jump
            jumpsAvailable--;
        }

        // tell the animator if we are grounded or not
        anim.SetBool("isGrounded", cc.isGrounded);

        // trigger attack animation
        if (Input.GetButtonDown("Fire1") && Time.timeScale > 0) // prevent press start in beginning trigger attack
        {
            //anim.SetBool("attack", true);
            anim.SetTrigger("hit1");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            //anim.SetBool("attack2", true);
            anim.SetTrigger("hit2");
        }

        movement.y = yVelocity;

        movement *= Time.deltaTime;

        cc.Move(movement);
    }

    // Set the rotation of the model to match the direction of the movement vector
    private void RotateModelToFaceMovement(Vector3 moveDirection)
    {
        // Determine the rotation needed to face the direction of movement (only XZ movement - ignore Y)
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));

        // set the model's rotation
        //model.transform.rotation = newRotation;

        // replace the above line with this one to enable smoothing
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, rotateToFaceMovementSpeed * Time.deltaTime);
    }

    // set the player's Y rotation (yaw) to be aligned with the camera's Y rotation
    private void RotatePlayerToFaceAwayFromCamera()
    {
        // isolate the camera's Y rotation
        Quaternion camRotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);

        // set the player's rotation
        //transform.rotation = camRotation;

        // replace the above line with this one to enable smoothing
        transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, rotateToFaceAwayFromCameraSpeed * Time.deltaTime);
    }

}
