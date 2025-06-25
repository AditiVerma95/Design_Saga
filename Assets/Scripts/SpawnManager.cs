
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager Instance;
    
    // Used for raycasting
    [SerializeField] private Camera camera;
    
    // Preview object variables
    public GameObject previewObject = null;
    public LayerMask RayCastLayer;
    //public LayerMask PreviewLayerobject;
    [SerializeField] private Transform rightHandController; // Assign this in the Inspector

    // Material variables
    public Material currentMat;
    public bool isSettingMaterial = false;
    
    
    private void OnEnable() {
        Debug.Log(UserInputManager.Instance);
        UserInputManager.Instance.spawnEvent += SpawnObject;
        UserInputManager.Instance.removeEvent += RemoveObject;
        UserInputManager.Instance.ApplyMaterialToObject += SetSelectedMaterial;
    }

    private void OnDisable() {
        UserInputManager.Instance.spawnEvent -= SpawnObject;
        UserInputManager.Instance.removeEvent -= RemoveObject;
        UserInputManager.Instance.ApplyMaterialToObject -= SetSelectedMaterial;

    }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        PreviewSpawningObject();
    }

    private void PreviewSpawningObject() {
        if (isSettingMaterial) {
            Destroy(previewObject);
            return;
        }
        if(UIManager.Instance.currentSelectedPrefab[0] == null) return;
        if (previewObject == null) {
            previewObject = Instantiate(UIManager.Instance.currentSelectedPrefab[0]);
        }
        Ray ray = new Ray(rightHandController.position, rightHandController.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f, RayCastLayer)) {
            previewObject.transform.position = hit.point;
            previewObject.transform.rotation = Quaternion.identity;
            previewObject.layer = 6;
            
        }
        
    }
    
    public void SetSelectedMaterial(object sender, EventArgs e) {
        if (!isSettingMaterial) return;
        
        if (Input.GetMouseButtonDown(0) && currentMat != null) {
            Ray ray = new Ray(rightHandController.position, rightHandController.forward);

            if (Physics.Raycast(ray, out RaycastHit hit)) {
           
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null) {
                    renderer.material = currentMat;
                    var materials = renderer.materials;
                    for (int i = 0; i < materials.Length; i++) {
                        if (materials[i].name.Contains("Glass") || materials[i].shader.name.Contains("Transparent")) continue;
                        materials[i] = currentMat;
                    }
                    renderer.materials = materials;


                }
            
            }
        }
    }
    
    public void SpawnObject(object sender, EventArgs e) {
        if(isSettingMaterial) return;
        
        Ray ray = new Ray(rightHandController.position, rightHandController.forward);
        Debug.Log("wa");
        if (Physics.Raycast(ray, out RaycastHit hit, 50f)) {
            Debug.Log(hit.point);
            GameObject spawned = Instantiate(UIManager.Instance.currentSelectedPrefab[1], hit.point, Quaternion.identity);
            spawned.tag = "SpawnedObject";
        }
    }
    public void RemoveObject(object sender, EventArgs e) {
        Ray ray = new Ray(rightHandController.position, rightHandController.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 50f)) {
            if (hit.collider.CompareTag("SpawnedObject")) {
                Debug.Log(hit.collider.name);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
