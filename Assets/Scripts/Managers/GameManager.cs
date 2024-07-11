using System;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

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

   [SerializeField]
   private float palyingTimerMax = 60f;

   private State state;
   private State lastState;
   private float lobbyTimer = 1f;
   private float countdownTimer = 3f;
   private float palyingTimer;

   private new void Awake()
   {
      base.Awake();
      state = State.Lobby;
      lastState = state;
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
               lastState = state;
               state = State.Countdown;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.Countdown:
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0f)
            {
               lastState = state;
               state = State.Playing;
               palyingTimer = palyingTimerMax;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.Playing:
            palyingTimer -= Time.deltaTime;
            if (palyingTimer < 0f)
            {
               lastState = state;
               state = State.GameOver;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.Paused:

            break;
         case State.GameOver:
            lastState = state;

            break;
      }
   }

   public bool IsGamePlaying()
   {
      return state == State.Playing;
   }


   public bool IsGamePaused()
   {
      return state == State.Paused;
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
      if (state == State.Paused)
      {
         state = lastState;
         lastState = State.Paused;
         OnStateChanged?.Invoke(this, EventArgs.Empty);
         OnGameUnpaused?.Invoke(this, EventArgs.Empty);
      }
      else
      {
         lastState = state;
         state = State.Paused;
         OnStateChanged?.Invoke(this, EventArgs.Empty);
         OnGamePaused?.Invoke(this, EventArgs.Empty);
      }
   }
}
