using System;
using UnityEngine;

public class GhostModeController : MonoBehaviour {
    [SerializeField] private Camera camera;
    [SerializeField] public float sensitivity = 4f;
    private float xRotation = 0f; // up/down
    private float yRotation = 0f; // left/right
    
    private void Start() {
        UserInputManager.Instance.spawnEvent += SpawnObject;
        UserInputManager.Instance.removeEvent += RemoveObject;
        
    }


    private void Update() {
        Vector2 look = UserInputManager.Instance.lookInput;
        Vector2 move = UserInputManager.Instance.moveInput;
        float mouseX = look.x * sensitivity * Time.deltaTime;
        float mouseY = look.y * sensitivity * Time.deltaTime;
        
        // Rotate up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        // Rotate left/right
        yRotation += mouseX;

        // Apply rotation
        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        // Movement logic
        
        // Movement logic
        Vector3 moveDirection = camera.transform.right * move.x + camera.transform.forward * move.y;
        transform.position += moveDirection * Time.deltaTime * sensitivity; // 5f is movement speed
        

    }

    private void SpawnObject(object sender, EventArgs e) {
        //setting up tail(origin)
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
       
        if (Physics.Raycast(ray,  out RaycastHit hit, 50f)) {
            Debug.Log(hit.point);
            Instantiate(UIManager.Instance.currentSelectedPrefab, hit.point, Quaternion.identity);
            GameObject spawned = Instantiate(UIManager.Instance.currentSelectedPrefab, hit.point, Quaternion.identity);
            spawned.tag = "SpawnedObject";
        }
    }
    private void RemoveObject(object sender, EventArgs e) {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
    
        if (Physics.Raycast(ray, out RaycastHit hit, 50f)) {
            // Optional: Only destroy objects with a specific tag to avoid deleting random scene stuff
            if (hit.collider.CompareTag("SpawnedObject")) {
                Destroy(hit.collider.gameObject);
            }
        }
    }

}
