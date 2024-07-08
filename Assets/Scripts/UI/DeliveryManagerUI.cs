using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
   [SerializeField]
   private Transform container;
   [SerializeField]
   private Transform recipeTemplate;

   private void Awake()
   {
      recipeTemplate.gameObject.SetActive(false);
   }

   private void Start()
   {
      DeliveryManager.Instance.OnCommandSpawned += DeliveryManager_OnCommandSpawned;
      DeliveryManager.Instance.OnCommandCompleted += DeliveryManager_OnCommandCompleted;
      UpdateVisual();
   }

   private void DeliveryManager_OnCommandCompleted(object sender, System.EventArgs e)
   {
      UpdateVisual();
   }

   private void DeliveryManager_OnCommandSpawned(object sender, System.EventArgs e)
   {
      UpdateVisual();
   }

   private void UpdateVisual()
   {
      foreach (Transform child in container)
      {
         if (child == recipeTemplate) continue;
         Destroy(child.gameObject);
      }

      foreach (RecipeSO recipe in DeliveryManager.Instance.GetCommandsList())
      {
         Transform recipeTransform = Instantiate(recipeTemplate, container);
         recipeTransform.gameObject.SetActive(true);
         recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipe);
      }
   }
}