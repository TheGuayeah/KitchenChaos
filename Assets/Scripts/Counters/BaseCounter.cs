using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
   [SerializeField]
   private Transform counterTopPoint;

   private KitchenObject kitchenObject;

   public virtual void Interact(Player player)
   {
      Debug.LogError("Interacting with BaseCounter");
   }

   public virtual void InteractAlternate(Player player)
   {
   }

   public Transform GetSpawnPoint()
   {
      return counterTopPoint;
   }

   public void SetKitchenObject(KitchenObject newKitchenObject)
   {
      kitchenObject = newKitchenObject;
   }

   public KitchenObject GetKitchenObject()
   {
      return kitchenObject;
   }

   public void ClearKitchenObject()
   {
      kitchenObject = null;
   }

   public bool HasKitchenObject()
   {
      return kitchenObject != null;
   }
}
