using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Singleton<DeliveryManager>
{
   public event EventHandler OnCommandSpawned;
   public event EventHandler OnCommandCompleted;

   [SerializeField]
   private RecipeListSO recipeListSO;

   private List<RecipeSO> commandsList;
   private float spwanRecipeTimer;
   private float spwanRecipeTimerMax = 4f;
   private int maxWaitingRecipes = 4;

   private new void Awake()
   {
      base.Awake();
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
            
            commandsList.Add(command);

            OnCommandSpawned?.Invoke(this, EventArgs.Empty);
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
               commandsList.RemoveAt(i);

               OnCommandCompleted?.Invoke(this, EventArgs.Empty);
               return;
            }
         }
      }
   }

   public List<RecipeSO> GetCommandsList()
   {
      return commandsList;
   }
}
