using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;
    [SerializeField]
    private Transform counterTopPoint;

    public void Interact()
    {
        Debug.Log("Interacted with ClearCounter");
        Transform kitchenObjectTransform = 
            Instantiate(kitchenObjectSO.prefab, counterTopPoint);

        if (counterTopPoint.childCount < 0)
        {
            kitchenObjectTransform.localPosition = Vector3.zero;
        }
    }
}
