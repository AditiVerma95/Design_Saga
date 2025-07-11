using System.Collections;
using UnityEngine;

public class GhostModeController : MonoBehaviour {
    public static GhostModeController Instance;
    
    [SerializeField] private Camera camera;
    
    [SerializeField] public float moveSpeed = 6f;
    
    
    private Vector2 move;
    private CharacterController controller;

    private bool isRotating = false;

    private void Start() {
        Instance = this;
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        move = UserInputManager.Instance.moveInput;
    }

    void FixedUpdate() {
        PlayerMovement();
        PlayerRotation();
    }

    private void PlayerMovement() {
        float currentSpeed = moveSpeed;
        Vector3 moveDir;
        if (UserInputManager.Instance.isMovingVertically) {
            moveDir = new Vector3(0f, currentSpeed * move.y, 0f);
        }
        else {
            // Move in the direction the camera is facing (ignoring vertical tilt)
            Vector3 forward = camera.transform.forward;
            Vector3 right = camera.transform.right;
        
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            moveDir = (right * move.x + forward * move.y) * currentSpeed;
            moveDir = new Vector3(moveDir.x, 0f, moveDir.z);
        }
        controller.Move(moveDir * Time.fixedDeltaTime);
    }

    private void PlayerRotation() {
        Vector2 rotateInput = UserInputManager.Instance.rotateInput;
        if (rotateInput.x != 0 && !isRotating) {
            StartCoroutine(RotatePlayer(rotateInput.x > 0 ? 1 : -1));
        }
    }

    private IEnumerator RotatePlayer(float dir) {
        // dir -> -1 or 1
        isRotating = true;
        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = initialRotation * Quaternion.Euler(0, 45f * dir, 0);

        float current = 0f;
        float duration = 0.2f;
        
        while (current < duration) {
            float t = current / duration;
            transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, t);
            current += Time.deltaTime;
            yield return null;
        }

        transform.rotation = finalRotation;
        isRotating = false;
    }
}

