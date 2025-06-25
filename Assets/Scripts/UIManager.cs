using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject inventoryPanel;
   private bool isInventoryOpen = false;
   
   [SerializeField] private GameObject kitchenItems;
   [SerializeField] private GameObject livingRoomItems;
   [SerializeField] private GameObject bedroomItems;
   [SerializeField] private GameObject Colours;
   [SerializeField] private Camera camera;

   private Button lastActiveButton;
 
   public static UIManager Instance;
   public GameObject[] currentSelectedPrefab;

  
   private void Awake() {
      Instance = this;
   }

   public void ApplyMaterial(Material material) {
      SpawnManager.Instance.currentMat = material;
      SpawnManager.Instance.isSettingMaterial = true;
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
      SpawnManager.Instance.isSettingMaterial = false;
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
   
   private void OpenInventory()
   {
      inventoryPanel.SetActive(true);
      isInventoryOpen = true;
      Cursor.lockState = CursorLockMode.None;
      UserInputManager.Instance.userInputActionAsset.Ghost.Disable();
   }
   
   private void CloseInventory()
   {
      inventoryPanel.SetActive(false);
      isInventoryOpen = false;
      Cursor.lockState = CursorLockMode.Locked;
      UserInputManager.Instance.userInputActionAsset.Ghost.Enable();
   }
   
   
   public void EnableKitchenItems(Button kitchenButton)
   {
      DisableEverything();
      kitchenItems.SetActive(true);
      kitchenButton.interactable = false;
      lastActiveButton = kitchenButton;
   }
   
   public void EnableLivingRoomItems(Button livingRoomButton)
   {
      DisableEverything();
      livingRoomItems.SetActive(true);
      livingRoomButton.interactable = false;
      lastActiveButton = livingRoomButton;
   }
   
   public void EnableBedroomItems(Button bedroomButton)
   {
      DisableEverything();
      bedroomItems.SetActive(true);
      bedroomButton.interactable = false;
      lastActiveButton = bedroomButton;
   }
   public void EnableColours(Button colorsButton)
   {
      DisableEverything();
      Colours.SetActive(true);
      colorsButton.interactable = false;
      lastActiveButton = colorsButton;
   }
   private void DisableEverything() {
      if(lastActiveButton != null) lastActiveButton.interactable = true;
      kitchenItems.SetActive(false);
      livingRoomItems.SetActive(false);
      bedroomItems.SetActive(false);
      Colours.SetActive(false);
   }
}
