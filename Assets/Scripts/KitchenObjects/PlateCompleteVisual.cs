using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
   [Serializable]
   public struct KitchenObjectSO_GameObject
   {
      public KitchenObjectSO kitchenObjectSO;
      public GameObject gameObject;
   }

   [SerializeField]
   private PlateKitchenObject plateKitchenObject;
   [SerializeField]
   private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjects;

   private void Start()
   {
      plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
   }

   private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
   {
      foreach (KitchenObjectSO_GameObject ingredient in kitchenObjectSO_GameObjects)
      {
         if (ingredient.kitchenObjectSO == e.ingredientToAdd)
         {
            ingredient.gameObject.SetActive(true);
            break;
         }
      }
   }
}
