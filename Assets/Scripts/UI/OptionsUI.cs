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

   #region BUTTONS
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
   [SerializeField]
   private Button gamepadInteractButton;
   [SerializeField]
   private Button gamepadInteractAltButton;
   [SerializeField]
   private Button gamepadPauseButton;
   #endregion

   #region BUTTON_TEXTS
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
   [SerializeField]
   private TextMeshProUGUI gamepadInteractText;
   [SerializeField]
   private TextMeshProUGUI gamepadInteractAltText;
   [SerializeField]
   private TextMeshProUGUI gamepadPauseText;
   #endregion

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

      gamepadInteractButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Gamepad_Interact, UpdateVisual);
      });

      gamepadInteractAltButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Gamepad_InteractAlternate, UpdateVisual);
      });

      gamepadPauseButton.onClick.AddListener(() =>
      {
         GameInput.Instance.RebindBinding(GameInput.Binding.Gamepad_Pause, UpdateVisual);
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

      gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
      gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
      gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
   }

   public void Show()
   {
      gameObject.SetActive(true);
      sfxSlider.Select();
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
