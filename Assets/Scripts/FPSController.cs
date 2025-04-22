using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80.0f;

    [Header("Input Handler (Optional Manual Drag)")]
    [SerializeField] private Movement inputHandler;

    private CharacterController characterController;
    private Camera mainCamera;

    private Vector3 currentMovement;
    private float verticalRotation = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        if (inputHandler == null)
        {
            inputHandler = Movement.Instance;
        }

        if (inputHandler == null)
        {
            inputHandler = FindObjectOfType<Movement>();
            Debug.LogWarning("Fallback: Movement.Instance was null, used FindObjectOfType<Movement>()");
        }

        if (inputHandler == null)
        {
            Debug.LogError("FPSController: No Movement input handler found in the scene!");
        }
    }

    private void Update()
    {
        if (inputHandler == null) return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 horizontalMovement = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        horizontalMovement = transform.forward * horizontalMovement.z + transform.right * horizontalMovement.x;
        horizontalMovement.Normalize();

        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);

        currentMovement.x = horizontalMovement.x * speed;
        currentMovement.z = horizontalMovement.z * speed;

        HandleJumping();

        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (inputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
