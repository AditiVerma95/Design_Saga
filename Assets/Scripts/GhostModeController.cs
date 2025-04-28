using System;
using UnityEngine;

public class GhostModeController : MonoBehaviour {
    [SerializeField] private Camera camera;
    [SerializeField] public float sensitivity = 4f;
    private float xRotation = 0f; // up/down
    private float yRotation = 0f; // left/right

    private void Update()
    {
        Vector2 look = UserInputManager.Instance.lookInput;

        float mouseX = look.x * sensitivity * Time.deltaTime;
        float mouseY = look.y * sensitivity * Time.deltaTime;

        // Rotate up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate left/right
        yRotation += mouseX;

        // Apply rotation
        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
