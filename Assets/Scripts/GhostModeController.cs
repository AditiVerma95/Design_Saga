using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GhostModeController : MonoBehaviour {
    [SerializeField] private Camera camera;
    //[SerializeField] public float sensitivity = 4f;
    [SerializeField] public float MoveSpeed = 6f;
    [SerializeField] public float sprintMultiplier = 2f;
    public static GhostModeController Instance;
    //private float xRotation = 0f;
    //private float yRotation = 0f;
    //private Vector2 look;
    private Vector2 move;
    private CharacterController controller;

    private void Start() {
        Instance = this;
        
        controller = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // Handle input
        //look = UserInputManager.Instance.lookInput;
        move = UserInputManager.Instance.moveInput;

        // // Mouse rotation
        // float mouseX = look.x * sensitivity * Time.deltaTime;
        // float mouseY = look.y * sensitivity * Time.deltaTime;
        //
        // xRotation -= mouseY;
        // xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // yRotation += mouseX;

        // Apply camera and body rotation
        //camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, camera.transform.rotation.y, 0f);
    }

    void FixedUpdate() {
        float currentSpeed = UserInputManager.Instance.isSprinting
            ? MoveSpeed * sprintMultiplier
            : MoveSpeed;

        // Move in the direction the camera is facing (ignoring vertical tilt)
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (right * move.x + forward * move.y) * currentSpeed;
        controller.Move(moveDir * Time.fixedDeltaTime);
    }
    

    
    
}

