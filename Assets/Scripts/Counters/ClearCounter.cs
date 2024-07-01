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
            if (!player.HasKitchenObject()) //Player is not carrying anything
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
