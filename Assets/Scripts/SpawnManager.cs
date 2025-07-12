
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
    public LayerMask SpawnableLayer;
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

        if (Physics.Raycast(ray, out RaycastHit hit, 50f, SpawnableLayer)) {
            previewObject.transform.position = hit.point;
            previewObject.transform.rotation = Quaternion.identity;
        }
        
    }
    
    public void SetSelectedMaterial(object sender, EventArgs e) {
        if (!isSettingMaterial) return;

        Ray ray = new Ray(rightHandController.position, rightHandController.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f)) {
            Debug.Log("Hit object: " + hit.collider.name);

            Renderer[] renderers = hit.collider.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) {
                Debug.LogWarning("❌ No Renderer found on hit object or its children.");
                return;
            }

            foreach (var renderer in renderers) {
                Material[] newMats = new Material[renderer.sharedMaterials.Length];
                for (int i = 0; i < newMats.Length; i++) {
                    newMats[i] = currentMat;
                }

                renderer.materials = newMats;
                Debug.Log($"✅ Applied material to {renderer.name} with {newMats.Length} slots.");
            }
        }
    }
 
    
    public void SpawnObject(object sender, EventArgs e) {
        if(isSettingMaterial) return;
        if(UIManager.Instance.currentSelectedPrefab[1] == null) return;
        Ray ray = new Ray(rightHandController.position, rightHandController.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 50f, SpawnableLayer)) {
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
