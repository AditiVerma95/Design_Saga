using UnityEngine;

public class GridDraggable : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private float gridSize = 1f;
    private float objectHeight;

    void Start()
    {
        cam = Camera.main;
        // Use object's center Y as base height
        objectHeight = GetComponent<Renderer>().bounds.size.y / 2;
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 rawPosition = hit.point;

                // Snap X and Z to nearest grid point
                float snappedX = Mathf.Round(rawPosition.x / gridSize) * gridSize;
                float snappedZ = Mathf.Round(rawPosition.z / gridSize) * gridSize;

                // Set final snapped position
                transform.position = new Vector3(snappedX, objectHeight, snappedZ);
            }
        }
    }
}