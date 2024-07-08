using UnityEngine;

public class DeliveryCounter : BaseCounter
{
   public static DeliveryCounter Instance { get; protected set; }

   protected void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         //DontDestroyOnLoad(this);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public override void Interact(Player player)
   {
      if (player.HasKitchenObject())
      {
         if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
         {
            DeliveryManager.Instance.DeliverRecipe(plate);
            player.GetKitchenObject().DestroySelf();
         }
      }
   }
}
