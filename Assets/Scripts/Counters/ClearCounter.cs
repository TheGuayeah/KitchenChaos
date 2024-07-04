using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) //There's no kitchen object here
        {
            if (player.HasKitchenObject()) //Player is carrying something
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else //There is a kitchen object here
        {
            if (player.HasKitchenObject()) //Player is carrying something
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                { //Player is carrying a plate
                    bool ingredientAdded = plate.TryAddIngredient(
                        GetKitchenObject().GetKitchenObjectSO());

                    if (ingredientAdded)
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else //Player is carrying something that is NOT a plate
                {
                    if (GetKitchenObject().TryGetPlate(out plate)) //Plate is on the counter
                    {
                        bool ingredientAdded = plate.TryAddIngredient(
                            player.GetKitchenObject().GetKitchenObjectSO());

                        if (ingredientAdded)
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else //Player is not carrying anything
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
