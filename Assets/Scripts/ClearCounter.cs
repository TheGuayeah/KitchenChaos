using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) //There's no kitchen object here
        {
            if (player.HasKitchenObject()) //Player is carrying something
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else //Player is not carrying anything
            {

            }
        }
        else //There is a kitchen object here
        {
            if (player.HasKitchenObject()) //Player is carrying something
            {

            }
            else //Player is not carrying anything
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
