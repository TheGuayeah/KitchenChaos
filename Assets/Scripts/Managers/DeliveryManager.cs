using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Singleton<DeliveryManager>
{
   public event EventHandler OnCommandSpawned;
   public event EventHandler OnCommandCompleted;
   public event EventHandler OnCommandSucceded;
   public event EventHandler OnCommandFailed;

   [SerializeField]
   private RecipeListSO recipeListSO;

   private List<RecipeSO> commandsList;
   private float spwanCommandTimer;
   private float spwanCommandTimerMax = 4f;
   private int maxWaitingCommands = 4;
   private int successfulDeliveries;

   private new void Awake()
   {
      base.Awake();
      commandsList = new List<RecipeSO>();
   }

   private void Update()
   {
      if (GameManager.Instance.IsGamePlaying())
      {
         SpawnCommands();
      }
   }

   private void SpawnCommands()
   {
      spwanCommandTimer -= Time.deltaTime;

      if (spwanCommandTimer <= 0f)
      {
         spwanCommandTimer = spwanCommandTimerMax;

         if (commandsList.Count < maxWaitingCommands)
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
               successfulDeliveries++;
               commandsList.RemoveAt(i);

               OnCommandCompleted?.Invoke(this, EventArgs.Empty);
               OnCommandSucceded?.Invoke(this, EventArgs.Empty);
               return;
            }
         }
      }

      OnCommandFailed?.Invoke(this, EventArgs.Empty);
   }

   public List<RecipeSO> GetCommandsList()
   {
      return commandsList;
   }

   public int GetSuccessfulDeliveries()
   {
      return successfulDeliveries;
   }
}
