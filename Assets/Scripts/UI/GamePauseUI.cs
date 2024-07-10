using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : Singleton<GamePauseUI>
{
   [SerializeField]
   private Button resumeButton;
   [SerializeField]
   private Button mainMenuButton;
   [SerializeField]
   private Button optionsButton;

   private new void Awake()
   {
      base.Awake();
      resumeButton.onClick.AddListener(() =>
      {
         GameManager.Instance.TogglePauseGame();
      });

      mainMenuButton.onClick.AddListener(() =>
      {
         Loader.LoadScene(Loader.Scene.MainMenuScene);
      });

      optionsButton.onClick.AddListener(() =>
      {
         OptionsUI.Instance.Show();
         Hide();
      });
   }

   private void Start()
   {
      GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
      GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
      Hide();
   }

   private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
   {
      Hide();
   }

   private void GameManager_OnGamePaused(object sender, System.EventArgs e)
   {
      Show();
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
