using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BurningRecipeSO", menuName = "SrictableObjects/BurningRecipeSO", order = 0)]
public class BurningRecipeSO : ScriptableObject
{
   public KitchenObjectSO input;
   public KitchenObjectSO output;
   public float burningTimer;
}
