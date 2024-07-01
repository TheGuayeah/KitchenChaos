using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;
    [SerializeField]
    private SpriteRenderer containerContent;

    private void OnValidate()
    {
        containerContent.sprite = kitchenObjectSO.sprite;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
