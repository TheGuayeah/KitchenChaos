using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newParent)
    {
        if (newParent.HasKitchenObject())
        {
            Debug.LogError(newParent + " already has " + 
                newParent.GetKitchenObject().name);
            return;
        }

        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }

        kitchenObjectParent = newParent;

        newParent.SetKitchenObject(this);

        transform.SetParent(newParent.GetSpawnPoint());
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetClearCounter()
    {
        return kitchenObjectParent;
    }
}
