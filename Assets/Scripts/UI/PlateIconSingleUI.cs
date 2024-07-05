using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
   [SerializeField]
   private Image image;

   public void SetIngredientUI(KitchenObjectSO ingredient)
   {
      image.sprite = ingredient.sprite;
   }
}
