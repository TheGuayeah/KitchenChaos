using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField]
   private KitchenObjectSO kitchenObjectSO;

   private IKitchenObjectParent kitchenObjectParent;

   public KitchenObjectSO GetKitchenObjectSO()
   {
      return kitchenObjectSO;
   }

   public void SetKitchenObjectParent(IKitchenObjectParent newParent)
   {
      if (newParent.HasKitchenObject())
      {
         Debug.LogError(newParent + " already has " +
             newParent.GetKitchenObject().name);
         return;
      }

      if (kitchenObjectParent != null)
      {
         kitchenObjectParent.ClearKitchenObject();
      }

      kitchenObjectParent = newParent;

      newParent.SetKitchenObject(this);

      transform.SetParent(newParent.GetSpawnPoint());
      transform.localPosition = Vector3.zero;
   }

   public IKitchenObjectParent GetClearCounter()
   {
      return kitchenObjectParent;
   }

   public void DestroySelf()
   {
      kitchenObjectParent.ClearKitchenObject();
      Destroy(gameObject);
   }

   public bool TryGetPlate(out PlateKitchenObject plate)
   {
      if (this is PlateKitchenObject)
      {
         plate = this as PlateKitchenObject;
         return true;
      }
      else
      {
         plate = null;
         return false;
      }
   }

   public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,
       IKitchenObjectParent kitchenObjectParent)
   {
      Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
      KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
      kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
      return kitchenObject;
   }
}
