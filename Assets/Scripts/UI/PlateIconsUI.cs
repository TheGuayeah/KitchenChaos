using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
   [SerializeField]
   private PlateKitchenObject plate;
   [SerializeField]
   private Transform iconTemplate;

   private void Awake()
   {
      iconTemplate.gameObject.SetActive(false);
   }

   private void Start()
   {
      plate.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
   }

   private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
   {
      UpdateVisual();
   }

   private void UpdateVisual()
   {
      foreach (Transform child in transform)
      {
         if (child == iconTemplate) continue;
         Destroy(child.gameObject);
      }
      foreach (KitchenObjectSO ingredient in plate.GetIngredientsOnPlate())
      {
         Transform iconTransform = Instantiate(iconTemplate, transform);
         iconTransform.gameObject.SetActive(true);
         iconTransform.GetComponent<PlateIconSingleUI>().SetIngredientUI(ingredient);
      }
   }
}
