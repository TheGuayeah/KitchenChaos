using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectSO cutKitchenObjectSO;

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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject()) //There's a kitchen object here
        {
            if (!player.HasKitchenObject()) //Player is not carrying anything
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            }
        }
    }
}
