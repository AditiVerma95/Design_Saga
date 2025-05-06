using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject inventoryPanel;
   private bool isInventoryOpen = false;
   
   [SerializeField] private GameObject kitchenItems;
   [SerializeField] private GameObject livingRoomItems;
   [SerializeField] private GameObject bedroomItems;
 
   public static UIManager Instance;
   public (GameObject preview, GameObject prefab) currentSelected;

   public void SetCurrentSelected(GameObject[] objects) {
      currentSelected.preview = objects[0];
      currentSelected.prefab = objects[1];
   }
   private void Awake() {
      Instance = this;
   }

   private void Start() {
      
      UserInputManager.Instance.enableDisabled += OpenCloseInventory;
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

   private void DisableEverything()
   {
      kitchenItems.SetActive(false);
      livingRoomItems.SetActive(false);
      bedroomItems.SetActive(false);
   }
}
