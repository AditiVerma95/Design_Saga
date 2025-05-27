
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager Instance;
    [SerializeField] private Camera camera;
    public GameObject previewObject = null;
    public LayerMask RayCastLayer;
    public LayerMask PreviewLayerobject;
    private void OnEnable() {
        UserInputManager.Instance.spawnEvent += SpawnObject;
        UserInputManager.Instance.removeEvent += RemoveObject;
    }

    private void OnDisable() {
        UserInputManager.Instance.spawnEvent -= SpawnObject;
        UserInputManager.Instance.removeEvent -= RemoveObject;
    }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        PreviewSpawningObject();
    }

    private void PreviewSpawningObject() {
        
        if(UIManager.Instance.currentSelectedPrefab[0] == null) return;
        if (previewObject == null) {
            previewObject = Instantiate(UIManager.Instance.currentSelectedPrefab[0]);
        }
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 50f, RayCastLayer)) {
            previewObject.transform.position = hit.point;
            previewObject.transform.rotation = Quaternion.identity;
            previewObject.layer = 6;
            Debug.Log(hit.collider.name);
        }
        
    }
    private void SpawnObject(object sender, EventArgs e) {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 50f)) {
            Debug.Log(hit.point);
            GameObject spawned = Instantiate(UIManager.Instance.currentSelectedPrefab[1], hit.point, Quaternion.identity);
            spawned.tag = "SpawnedObject";
        }
    }
    private void RemoveObject(object sender, EventArgs e) {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 50f)) {
            if (hit.collider.CompareTag("SpawnedObject")) {
                Debug.Log(hit.collider.name);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
