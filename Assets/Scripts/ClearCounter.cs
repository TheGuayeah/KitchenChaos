using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;
    [SerializeField]
    private Transform counterTopPoint;
    [SerializeField]
    private ClearCounter nextClearCounter;
    [SerializeField]
    private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.Space))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(nextClearCounter);
            }
        }
    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = 
                Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.name + " is in " + 
                kitchenObject.GetClearCounter().name);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
