using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Have set default walk and run speed so it looks about right with the animations
    public float walkSpeed = 2.0f; 
    public float runSpeed = 4.0f;
    public float jumpHeight = 5.0f;
    public float cameraHeight = 15.0f;
    public float cameraDistance = 7.5f;
    public bool invertCameraXAxis = false;
    public bool invertCameraYAxis = false;
    public float rotationSpeed = 120.0f;
    public Camera mainCamera;
    private Animator anim;
    
    private float moveSpeed;


    private Vector3 cameraTargetPosition;
    Transform cameraTransform;

    Collider playerCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
        moveSpeed = walkSpeed;
        cameraTransform = mainCamera.transform;
        // Set the camera target position to the initial camera position
        cameraTargetPosition = cameraTransform.position;
    }

    void Update()
    {
        // --------------------------------------
        // -----------Movement Controls----------
        // --------------------------------------
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the camera's forward and right directions
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward.Normalize();
        Vector3 cameraRight = mainCamera.transform.right;

        // Handle player movement
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            0.0f,
            Input.GetAxis("Vertical")
        );
        Vector3 movementDirection = cameraForward * movement.z + cameraRight * movement.x;
        movementDirection.y = 0.0f; // Prevent player from moving up/down
        movementDirection.Normalize();
        transform.position += movementDirection * moveSpeed * Time.deltaTime;

        // ------------------------------------
        // ---------Animation Controls---------
        // ------------------------------------
        if (movementDirection != Vector3.zero)
        {
            // Also controlling the player model's rotation here
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movementDirection),
                rotationSpeed * Time.deltaTime
            );


            anim.SetBool("isWalking", true);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isRunning", true);
                moveSpeed = runSpeed;
            }
            else
            {
                anim.SetBool("isRunning", false);
                moveSpeed = walkSpeed;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("isJumping");
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // ---------------------------------
        // ---------Camera Controls---------
        // ---------------------------------
        float cameraXOffset = cameraDistance;
        float cameraYOffset = cameraDistance;

        // Inverting the camera axis
        if(invertCameraXAxis){
            cameraXOffset = -cameraDistance;
        }
        if(invertCameraYAxis){
            cameraYOffset = -cameraDistance;
        }

        // Set the camera position and target
        cameraTransform.position = new Vector3(
            transform.position.x - cameraXOffset,
            transform.position.y + cameraHeight,
            transform.position.z + cameraYOffset
        );
        cameraTransform.LookAt(transform.position);
    }
}
