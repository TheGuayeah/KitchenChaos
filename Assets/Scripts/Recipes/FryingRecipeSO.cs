using UnityEngine;

[CreateAssetMenu(fileName = "New FryingRecipeSO", menuName = "SrictableObjects/FryingRecipeSO", order = 0)]
public class FryingRecipeSO : ScriptableObject
{
	public KitchenObjectSO input;
	public KitchenObjectSO output;
	public float fryingTimer;
}
