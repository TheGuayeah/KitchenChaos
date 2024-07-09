using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   public event EventHandler OnStateChanged;
   public event EventHandler OnGamePaused;
   public event EventHandler OnGameUnpaused;

   private enum State {
      Lobby,
      Countdown,
      Playing,
      Paused,
      GameOver
   }

   private State state;
   private float lobbyTimer = 1f;
   private float countdownTimer = 3f;
   private float palyingTimer;
   private float palyingTimerMax = 10f;
   private bool isGamePaused;

   private new void Awake()
   {
      base.Awake();
      state = State.Lobby;
   }

   private void Start()
   {
      GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
   }

   private void GameInput_OnPauseAction(object sender, EventArgs e)
   {
      TogglePauseGame();
   }

   private void Update()
   {
      switch (state)
      {
         case State.Lobby:
            lobbyTimer -= Time.deltaTime;
            if(lobbyTimer < 0f)
            {
               state = State.Countdown;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.Countdown:
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0f)
            {
               state = State.Playing;
               palyingTimer = palyingTimerMax;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.Playing:
            palyingTimer -= Time.deltaTime;
            if (palyingTimer < 0f)
            {
               state = State.GameOver;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.Paused:
            
            break;
         case State.GameOver:
            
            break;
      }
   }

   public bool IsGamePlaying()
   {
      return state == State.Playing;
   }

   public bool IsCountdownActive()
   {
      return state == State.Countdown;
   }

   public bool IsGameOver()
   {
      return state == State.GameOver;
   }

   public float GetCountdownTimer()
   {
      return countdownTimer;
   }

   public float GetPlayingTimerNormalized()
   {
      return 1 - (palyingTimer / palyingTimerMax);
   }

   public void TogglePauseGame()
   {
      isGamePaused = !isGamePaused;
      if (isGamePaused)
      {
         Time.timeScale = 0f;
         OnGamePaused?.Invoke(this, EventArgs.Empty);
      }
      else
      {
         Time.timeScale = 1f;
         OnGameUnpaused?.Invoke(this, EventArgs.Empty);
      }
   }
}
