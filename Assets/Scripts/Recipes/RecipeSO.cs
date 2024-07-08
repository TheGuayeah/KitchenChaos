using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RecipeSO", menuName = "SrictableObjects/RecipeSO", order = 0)]
public class RecipeSO : ScriptableObject
{
   public List<KitchenObjectSO> ingredients;
   public string recipeName;
}
