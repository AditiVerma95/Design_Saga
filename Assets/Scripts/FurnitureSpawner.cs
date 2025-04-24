using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    public GameObject[] furniturePrefabs;  // Drag prefabs here in inspector
    public Transform spawnPoint;          // Where to place new objects (e.g. in front of camera)

    public void SpawnFurniture(int index)
    {
        if (index >= 0 && index < furniturePrefabs.Length)
        {
            Debug.Log(index);
            Instantiate(furniturePrefabs[index], spawnPoint.position, Quaternion.identity);
        }
    }
}