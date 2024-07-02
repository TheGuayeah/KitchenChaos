using UnityEngine;

[CreateAssetMenu(fileName = "New CuttingRecipeSO", menuName = "SrictableObjects/CuttingRecipeSO", order = 0)]
public class CuttingRecipeSO : ScriptableObject
{
	public KitchenObjectSO input;
	public KitchenObjectSO output;
	public int cuttingTime;
}
