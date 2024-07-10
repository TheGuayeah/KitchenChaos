using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : Singleton<OptionsUI>
{
   [Header("Sliders")]
   [SerializeField]
   private Slider sfxSlider;
   [SerializeField]
   private Slider musicSlider;

   [Header("Texts")]
   [SerializeField]
   private TextMeshProUGUI sfxText;
   [SerializeField]
   private TextMeshProUGUI musicText;
   [SerializeField]
   private TextMeshProUGUI moveUpText;
   [SerializeField]
   private TextMeshProUGUI moveDownText;
   [SerializeField]
   private TextMeshProUGUI moveLeftText;
   [SerializeField]
   private TextMeshProUGUI moveRightText;
   [SerializeField]
   private TextMeshProUGUI interactText;
   [SerializeField]
   private TextMeshProUGUI interactAltText;
   [SerializeField]
   private TextMeshProUGUI pauseText;

   [Header("Buttons")]
   [SerializeField]
   private Button closeButton;
   [SerializeField]
   private Button moveUpButton;
   [SerializeField]
   private Button moveDownButton;
   [SerializeField]
   private Button moveLeftButton;
   [SerializeField]
   private Button moveRightButton;
   [SerializeField]
   private Button interactButton;
   [SerializeField]
   private Button interactAltButton;
   [SerializeField]
   private Button pauseButton;

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

      moveUpButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Move_Up, UpdateVisual);
      });

      moveDownButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Move_Down, UpdateVisual);
      });

      moveLeftButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Move_Left, UpdateVisual);
      });

      moveRightButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Move_Right, UpdateVisual);
      });

      interactButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Interact, UpdateVisual);
      });

      interactAltButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Interact_Alternate, UpdateVisual);
      });

      pauseButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Pause, UpdateVisual);
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

      moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
      moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
      moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
      moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
      interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
      interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
      pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
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
