using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Singleton<DeliveryManager>
{
   [SerializeField]
   private RecipeListSO recipeListSO;

   private List<RecipeSO> commandsList;
   private float spwanRecipeTimer;
   private float spwanRecipeTimerMax = 4f;
   private int maxWaitingRecipes = 4;

   private void Start()
   {
      commandsList = new List<RecipeSO>();
   }

   private void Update()
   {
      spwanRecipeTimer -= Time.deltaTime;

      if (spwanRecipeTimer <= 0f)
      {
         spwanRecipeTimer = spwanRecipeTimerMax;

         if (commandsList.Count < maxWaitingRecipes)
         {
            RecipeSO command = recipeListSO.recipes[Random.Range(0, recipeListSO.recipes.Count)];
            Debug.Log(command.name);
            commandsList.Add(command);
         }         
      }
   }

   public void DeliverRecipe(PlateKitchenObject plate)
   {
      for (int i = 0; i < commandsList.Count; i++)
      {
         RecipeSO command = commandsList[i];

         if(command.ingredients.Count == plate.GetIngredientsOnPlate().Count)
         {
            bool plateContentMatchesRecipe = true;

            foreach (KitchenObjectSO ingredient in command.ingredients)
            {
               bool ingredientFound = plate.TryFindIngredientOnPlate(ingredient);

               if (ingredientFound) break;
               else
               {
                  plateContentMatchesRecipe = false;
               }
            }
            if (plateContentMatchesRecipe)
            {
               Debug.Log("Recipe Delivered");
               commandsList.RemoveAt(i);
               return;
            }
         }
      }

      Debug.Log("Player delivered the WRONG recipe!");
   }
}
