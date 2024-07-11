using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameStartCountdownUI : MonoBehaviour
{
   private const string NUMBER_POPUP = "NumberPopup";

   [SerializeField]
   private TextMeshProUGUI countdownText;

   private Animator animator;
   private int lastCountdownNumber;

   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   private void Start()
   {
      GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
      Hide();
   }

   private void Update()
   {
      if (GameManager.Instance.IsCountdownActive())
      {
         int coundownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownTimer());
         countdownText.text = coundownNumber.ToString("F0");

         if(lastCountdownNumber != coundownNumber)
         {
            lastCountdownNumber = coundownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
         }
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
