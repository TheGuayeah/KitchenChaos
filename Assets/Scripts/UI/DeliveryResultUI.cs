using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DeliveryResultUI : MonoBehaviour
{
   private const string POPUP = "Popup";

   [SerializeField]
   private Image backgroundImage;
   [SerializeField]
   private Image iconImage;
   [SerializeField]
   private TextMeshProUGUI messageText;

   [Header("Success")]
   [SerializeField]
   private Color successColor;
   [SerializeField]
   private Sprite successSprite;

   [Header("Failed")]
   [SerializeField]
   private Color failedColor;
   [SerializeField]
   private Sprite failedSprite;

   private Animator animator;

   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   private void Start()
   {
      DeliveryManager.Instance.OnCommandSucceded += DeliveryManager_OnCommandSucceded;
      DeliveryManager.Instance.OnCommandFailed += DeliveryManager_OnCommandFailed;

      gameObject.SetActive(false);
   }

   private void DeliveryManager_OnCommandFailed(object sender, System.EventArgs e)
   {
      gameObject.SetActive(true);
      animator.SetTrigger(POPUP);
      backgroundImage.color = failedColor;
      iconImage.sprite = failedSprite;
      messageText.text = "Delivery\nFailed";
   }

   private void DeliveryManager_OnCommandSucceded(object sender, System.EventArgs e)
   {
      gameObject.SetActive(true);
      animator.SetTrigger(POPUP);
      backgroundImage.color = successColor;
      iconImage.sprite = successSprite;
      messageText.text = "Delivery\nSuccess";
   }
}
