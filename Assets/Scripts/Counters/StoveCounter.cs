using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
   public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
   public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
   public class OnStateChangedEventArgs : EventArgs
   {
      public State stateChanged;
   }

   public enum State
   {
      Idle,
      Cooking,
      Fried,
      Burned
   }
   [SerializeField] private State state;
   [SerializeField] private FryingRecipeSO[] fryingRecipesSO;
   [SerializeField] private BurningRecipeSO[] burningRecipesSO;

   private FryingRecipeSO fryingRecipe;
   private BurningRecipeSO burningRecipe;
   private float fryingTimer;
   private float burningTimer;

   private void Start()
   {
      state = State.Idle;
   }

   private void Update()
   {
      if (HasKitchenObject() && GameManager.Instance.IsGamePlaying())
      {
         switch (state)
         {
            case State.Idle:
               break;
            case State.Cooking:
               fryingTimer += Time.deltaTime;

               OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
               {
                  progressNormalized = fryingTimer / fryingRecipe.fryingTimer
               });

               if (fryingTimer >= fryingRecipe.fryingTimer)
               {
                  GetKitchenObject().DestroySelf();

                  KitchenObject.SpawnKitchenObject(fryingRecipe.output, this);

                  burningRecipe = GetBurningRecipe(GetKitchenObject().GetKitchenObjectSO());
                  burningTimer = 0f;
                  state = State.Fried;

                  OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                  {
                     stateChanged = state
                  });
               }
               break;
            case State.Fried:
               burningTimer += Time.deltaTime;

               OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
               {
                  progressNormalized = burningTimer / burningRecipe.burningTimer
               });

               if (burningTimer >= burningRecipe.burningTimer)
               {
                  GetKitchenObject().DestroySelf();

                  KitchenObject.SpawnKitchenObject(burningRecipe.output, this);

                  state = State.Burned;

                  OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                  {
                     stateChanged = state
                  });

                  OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                  {
                     progressNormalized = 0f
                  });
               }
               break;
            case State.Burned:
               break;
         }
      }
   }

   public override void Interact(Player player)
   {
      if (!HasKitchenObject()) //There's no kitchen object here
      {
         if (player.HasKitchenObject()) //Player is carrying something
         {
            KitchenObjectSO playerIngredient = player.GetKitchenObject().GetKitchenObjectSO();
            if (CanFryIngredient(playerIngredient)) //There's a recipe that can be fried
            {
               player.GetKitchenObject().SetKitchenObjectParent(this);
               fryingRecipe = GetFryingRecipe(GetKitchenObject()
                               .GetKitchenObjectSO());

               state = State.Cooking;
               fryingTimer = 0f;

               OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
               {
                  stateChanged = state
               });

               OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
               {
                  progressNormalized = fryingTimer / fryingRecipe.fryingTimer
               });
            }
         }
      }
      else //There is a kitchen object here
      {
         if (player.HasKitchenObject()) //Player is carrying something
         {
            PlateKitchenObject plate = null;
            if (player.GetKitchenObject().TryGetPlate(out plate))
            {
               bool ingredientAdded = plate.TryAddIngredient(
                   GetKitchenObject().GetKitchenObjectSO());

               if (ingredientAdded)
               {
                  GetKitchenObject().DestroySelf();

                  SwitchOffStove();
               }
            }
         }
         //Player is not carrying anything AND stove is not cooking
         if (!player.HasKitchenObject() && state != State.Cooking)
         {
            GetKitchenObject().SetKitchenObjectParent(player);

            SwitchOffStove();
         }
      }
   }

   private void SwitchOffStove()
   {
      state = State.Idle;

      OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
      {
         stateChanged = state
      });

      OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
      {
         progressNormalized = 0f
      });
   }

   private bool CanFryIngredient(KitchenObjectSO input)
   {
      FryingRecipeSO fryingRecipe = GetFryingRecipe(input);

      return fryingRecipe != null;
   }

   private KitchenObjectSO FriedIngredient(KitchenObjectSO input)
   {
      FryingRecipeSO fryingRecipe = GetFryingRecipe(input);

      return fryingRecipe ? fryingRecipe.output : null;
   }

   private FryingRecipeSO GetFryingRecipe(KitchenObjectSO input)
   {
      foreach (FryingRecipeSO recipe in fryingRecipesSO)
      {
         if (recipe.input == input)
         {
            return recipe;
         }
      }
      return null;
   }

   private BurningRecipeSO GetBurningRecipe(KitchenObjectSO input)
   {
      foreach (BurningRecipeSO recipe in burningRecipesSO)
      {
         if (recipe.input == input)
         {
            return recipe;
         }
      }
      return null;
   }
}
