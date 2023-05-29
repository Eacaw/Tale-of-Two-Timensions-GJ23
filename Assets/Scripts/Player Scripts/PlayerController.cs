using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference TeleportEventPath;

    public float cameraHeight = 15.0f;
    public float cameraDistance = 7.5f;
    public bool invertCameraXAxis = false;
    public bool invertCameraYAxis = false;
    public float rotationSpeed = 120.0f;
    public Camera mainCamera;
    private Animator anim;

    public GameObject GUI;
    public GameObject tpGUI;
    public Animator tpAnim;

    private float moveSpeed;
    private float walkSpeed = 10.0f;
    private float runSpeed = 16.0f;
    private float jumpHeight = 7.5f;
    private bool inputDisabled = false;
    private float gravity = 5f;

    public bool hasBackpack = false;
    public bool hasKey = false;
    public bool hasAmulet = false;

    public GameObject backpackUIObject;
    public GameObject keyUIObject;
    public GameObject amuletUIObject;
    public GameObject poisonUIObject;
    public GameObject poisonRumUIObject;
    public GameObject wizardJuiceUIObject;

    public TMP_Text textMesh;
    public TMP_Text yearIndicator;
    public CanvasRenderer yearIndicatorBackground;
    public string currentYear = "2023";

    private Vector3 cameraTargetPosition;
    Transform cameraTransform;

    Collider playerCollider;
    Rigidbody playerRigidBody;

    // ------------------------------------------
    // Player checkpoint tracking
    // ------------------------------------------
    public int currentCheckpoint = 0;
    public bool hasKilledBlacksmith = false;
    public bool hasTriggeredStartTrigger = false;

    private FMOD.Studio.Bus masterBus;
    private FMOD.Studio.Bus ambienceBus;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        anim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
        playerRigidBody = GetComponent<Rigidbody>();
        moveSpeed = walkSpeed;
        cameraTransform = mainCamera.transform;
        // Set the camera target position to the initial camera position
        cameraTargetPosition = cameraTransform.position;

        backpackUIObject.SetActive(false);
        keyUIObject.SetActive(false);
        amuletUIObject.SetActive(false);
        poisonUIObject.SetActive(false);
        poisonRumUIObject.SetActive(false);
        wizardJuiceUIObject.SetActive(false);

        textMesh.gameObject.SetActive(false);
        yearIndicator.gameObject.SetActive(false); 
        yearIndicatorBackground.gameObject.SetActive(false);

        CloseTpGUI();

        masterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
        ambienceBus = FMODUnity.RuntimeManager.GetBus("port:/Ambience");
    }

    void Update()
    {
        // Disable player move inputs if game is stopped
        inputDisabled = Time.timeScale == 0f;

        // Calculate the camera's forward and right directions
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward.Normalize();
        Vector3 cameraRight = mainCamera.transform.right;

        // Get player movement
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            0.0f,
            Input.GetAxis("Vertical")
        );

        if (!inputDisabled)
        {
            MovePlayer(movement, cameraForward, cameraRight);
            SetAnimation(movement);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                TeleportPlayer();
            }
        }

        SetCamera();

        textMesh.text = currentYear;
    }

    public void SetBackpackGUI(bool alpha)
    {
        backpackUIObject.SetActive(alpha);
    }

    public void SetAmuletGUI(bool alpha)
    {
        amuletUIObject.SetActive(alpha);
    }

    public void SetPoisonGUI(bool alpha)
    {
        poisonUIObject.SetActive(alpha);
    }

    public void SetKeyGUI(bool alpha)
    {
        keyUIObject.SetActive(alpha);
    }

    public void SetPoisionRumGUI(bool alpha)
    {
        poisonRumUIObject.SetActive(alpha);
    }

    public void SetWizardJuicGUI(bool alpha)
    {
        wizardJuiceUIObject.SetActive(alpha);
    }

    public void HideGUI()
    {
        GUI.SetActive(false);
    }

    public void ShowGUI()
    {
        GUI.SetActive(true);
    }

    void Jump()
    {
        // If the player is jumping or falling, you can't jump
        if (playerRigidBody.velocity.y <= 0 && playerRigidBody.velocity.y > -0.25f)
        {
            anim.SetTrigger("isJumping");
            playerRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }
    }

    void MovePlayer(Vector3 movement, Vector3 cameraForward, Vector3 cameraRight)
    {
        if (movement != Vector3.zero)
        {
            Vector3 movementDirection = cameraForward * movement.z + cameraRight * movement.x;
            movementDirection.Normalize();

            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            float speed = isRunning ? runSpeed : walkSpeed;

            playerRigidBody.velocity = new Vector3(
                movementDirection.x * speed,
                playerRigidBody.velocity.y,
                movementDirection.z * speed
            );
            playerRigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movementDirection),
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            playerRigidBody.velocity = new Vector3(0, playerRigidBody.velocity.y, 0);
            playerRigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    void SetAnimation(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
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
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
    }

    void SetCamera()
    {
        float cameraXOffset = cameraDistance;
        float cameraYOffset = cameraDistance;

        // Inverting the camera axis
        if (invertCameraXAxis)
        {
            cameraXOffset = -cameraDistance;
        }
        if (invertCameraYAxis)
        {
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

    void TeleportPlayer()
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            DialogueTrigger dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
            if (dialogueTrigger != null && hasAmulet == false)
            {
                dialogueTrigger.TriggerDialogue();
                return;
            }
            tpGUI.SetActive(true);
            tpAnim.SetTrigger("Opening");
            Invoke("CloseTpGUI", 1);
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                currentYear = "1550";
                SceneManager.LoadScene(2);
            }
            else
            {
                currentYear = "1570";
                SceneManager.LoadScene(1);
            }
            PlayTeleport();
        }
    }

    void CloseTpGUI()
    {
        tpGUI.SetActive(false);
    }

    void PlayTeleport()
    {
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambienceBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        FMOD.Studio.EventInstance teleport = FMODUnity.RuntimeManager.CreateInstance(
            TeleportEventPath
        );
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(
            teleport,
            transform,
            GetComponent<Rigidbody>()
        );

        teleport.start();
        teleport.release();
    }
}
