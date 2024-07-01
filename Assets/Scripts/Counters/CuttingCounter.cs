using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]
    private CuttingRecipeSO[] cuttingRecipesSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) //There's no kitchen object here
        {
            if (player.HasKitchenObject()) //Player is carrying something
            {
                if (CanCutIngredient(player.GetKitchenObject()
                    .GetKitchenObjectSO())) //There's a recipe that can be cut
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
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
        if (HasKitchenObject() && //There's a kitchen object here AND
            CanCutIngredient(GetKitchenObject().GetKitchenObjectSO())) //Can be cut
        {
            if (!player.HasKitchenObject()) //Player is not carrying anything
            {
                KitchenObjectSO output = CutIngredient(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(output, this);
            }
        }
    }

    private bool CanCutIngredient(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipesSO)
        {
            if (recipe.input == input)
            {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO CutIngredient(KitchenObjectSO input)
    {
       foreach (CuttingRecipeSO recipe in cuttingRecipesSO)
        {
            if (recipe.input == input)
            {
                return recipe.output;
            }
        }
        return null;
    }
}
