using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject kitchenItems;
   [SerializeField] private GameObject livingRoomItems;
   [SerializeField] private GameObject bedroomItems;

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
