using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : Singleton<OptionsUI>
{
   [SerializeField]
   private Slider sfxSlider;
   [SerializeField]
   private Slider musicSlider;
   [SerializeField]
   private Button closeButton;
   [SerializeField]
   private TextMeshProUGUI sfxText;
   [SerializeField]
   private TextMeshProUGUI musicText;

   private new void Awake()
   {
      base.Awake();

      sfxSlider.value = SoundManager.Instance.GetVolume();
      musicSlider.value = MusicManager.Instance.GetVolume();

      sfxSlider.onValueChanged.AddListener(delegate
      { 
         SoundManager.Instance.ChangeVolume(sfxSlider.value);
         UpdateVisual();
      });

      musicSlider.onValueChanged.AddListener(delegate
      {
         MusicManager.Instance.ChangeVolume(musicSlider.value);
         UpdateVisual();
      });

      closeButton.onClick.AddListener(() =>
      {
         GamePauseUI.Instance.Show();
         Hide();
      });
   }

   private void Start()
   {
      GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
      UpdateVisual();
      Hide();
   }

   private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
   {
      Hide();
   }

   private void UpdateVisual()
   {
      sfxText.text = "Sound Effects: " + 
         Mathf.Round(SoundManager.Instance.GetVolume() * 100f);
      musicText.text = "Music: " +
         Mathf.Round(MusicManager.Instance.GetVolume() * 100f);
   }

   public void Show()
   {
      gameObject.SetActive(true);
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
