using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;


    [SerializeField]
    private CuttingRecipeSO[] cuttingRecipesSO;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) //There's no kitchen object here
        {
            if (player.HasKitchenObject()) //Player is carrying something
            {
                KitchenObjectSO playerIngredient = player.GetKitchenObject().GetKitchenObjectSO();
                if (CanCutIngredient(playerIngredient)) //There's a recipe that can be cut
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipe = GetCuttingRecipe(playerIngredient);
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingTime
                    });
                }
            }
        }
        else //There is a kitchen object here
        {
            if (!player.HasKitchenObject()) //Player is not carrying anything
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO ingredient = GetKitchenObject().GetKitchenObjectSO();
            if (CanCutIngredient(ingredient))
            {
                if (!player.HasKitchenObject()) //Player is not carrying anything
                {
                    cuttingProgress++;

                    OnCut?.Invoke(this, EventArgs.Empty);

                    CuttingRecipeSO cuttingRecipe = GetCuttingRecipe(ingredient);
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingTime
                    });

                    if (cuttingProgress >= cuttingRecipe.cuttingTime)
                    {
                        KitchenObjectSO output = CutIngredient(ingredient);
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(output, this);
                    }
                }
            }
        }
    }

    private bool CanCutIngredient(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipe(input);

        return cuttingRecipe != null;
    }

    private KitchenObjectSO CutIngredient(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipe(input);

        return cuttingRecipe ? cuttingRecipe.output : null;
    }

    private CuttingRecipeSO GetCuttingRecipe(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipesSO)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }
}
