using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
   [SerializeField]
   private List<KitchenObjectSO> validIngredientsSO;

   private List<KitchenObjectSO> ingredientsOnPlateSO;

   private void Awake()
   {
      ingredientsOnPlateSO = new List<KitchenObjectSO>();
   }

   public bool TryAddIngredient(KitchenObjectSO ingredient)
   {
      if (!validIngredientsSO.Contains(ingredient)) return false; //Not a valid ingredient

      if (ingredientsOnPlateSO.Contains(ingredient)) return false; //Handle duplicates
      else
      {
         ingredientsOnPlateSO.Add(ingredient);
         return true;
      }
   }
}
