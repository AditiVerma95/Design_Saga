using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject inventoryPanel;
   private bool isInventoryOpen = false;
   
   [SerializeField] private GameObject kitchenItems;
   [SerializeField] private GameObject livingRoomItems;
   [SerializeField] private GameObject bedroomItems;
   [SerializeField] private GameObject Colours;
 
   public static UIManager Instance;
   public GameObject[] currentSelectedPrefab;

  
   private void Awake() {
      Instance = this;
   }

   private void Start() {
      
      UserInputManager.Instance.enableDisabled += OpenCloseInventory;
      currentSelectedPrefab = new GameObject[2];
   }

   public void SetPreviewObject(GameObject previewObject) {
      Destroy(SpawnManager.Instance.previewObject);
      SpawnManager.Instance.previewObject = null;
      currentSelectedPrefab[0] = previewObject;
      
   }
   public void SetSpawnObject(GameObject spawnObject) {
      currentSelectedPrefab[1] = spawnObject;
   }
   private void OpenCloseInventory(object sender, EventArgs e) {
      if (isInventoryOpen)
      {
         CloseInventory();
      }
      else
      {
         OpenInventory();
      }
   }
   
   public void SetMaterialToSelectedPrefab(Material mat)
   {
      if (currentSelectedPrefab[1] == null)
      {
         Debug.LogWarning("No prefab selected.");
         return;
      }

      Renderer renderer = currentSelectedPrefab[1].GetComponent<Renderer>();
      if (renderer != null)
      {
         renderer.material = mat;
      }
      else
      {
         Debug.LogWarning("Selected prefab has no Renderer.");
      }
   }

   //public void SpawnGameObject(GameObject prefab) {
      //currentSelectedPrefab[1] = prefab;
   //}
   
   private void OpenInventory()
   {
      inventoryPanel.SetActive(true);
      //InputManager.Instance.playerInputAction.Player.Disable();
      isInventoryOpen = true;
      Cursor.lockState = CursorLockMode.None;
      UserInputManager.Instance.userInputActionAsset.Ghost.Disable();
   }
   
   private void CloseInventory()
   {
      inventoryPanel.SetActive(false);
      //InputManager.Instance.playerInputAction.Player.Enable();
      isInventoryOpen = false;
      Cursor.lockState = CursorLockMode.Locked;
      UserInputManager.Instance.userInputActionAsset.Ghost.Enable();
   }
   
   
   public void EnableKitchenItems()
   {
      DisableEverything();
      kitchenItems.SetActive(true);
   }
   
   public void EnableLivingRoomItems()
   {
      DisableEverything();
      livingRoomItems.SetActive(true);
   }
   
   public void EnableBedroomItems()
   {
      DisableEverything();
      bedroomItems.SetActive(true);
   }
   public void EnableColours()
   {
      DisableEverything();
      Colours.SetActive(true);
   }
   private void DisableEverything()
   {
      kitchenItems.SetActive(false);
      livingRoomItems.SetActive(false);
      bedroomItems.SetActive(false);
      Colours.SetActive(false);
   }
}
