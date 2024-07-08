using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI countdownText;

   private void Start()
   {
      GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
      Hide();
   }

   private void Update()
   {
      if (GameManager.Instance.IsCountdownActive())
      {
         countdownText.text = GameManager.Instance.GetCountdownTimer().ToString("F0");
      }
   }

   private void GameManager_OnStateChanged(object sender, System.EventArgs e)
   {
      if (GameManager.Instance.IsCountdownActive())
      {
         Show();
      }
      else
      {
         Hide();
      }
   }

   private void Show()
   {
      gameObject.SetActive(true);
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
