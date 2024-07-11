using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoveCounterSound : MonoBehaviour
{
   [SerializeField]
   private StoveCounter stoveCounter;

   private AudioSource audioSource;
   private float warningSoundTimer;
   private bool playWarningSound;

   private void Awake()
   {
      audioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
      stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
   }

   private void Update()
   {
      if (playWarningSound)
      {
         warningSoundTimer -= Time.deltaTime;
         if (warningSoundTimer < 0f)
         {
            float warningSoundTimerMax = 0.2f;
            warningSoundTimer = warningSoundTimerMax;

            SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
         } 
      }
   }

   private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
   {
      float burnShowProgress = 0.5f;
      playWarningSound = GameManager.Instance.IsGamePlaying() && 
         stoveCounter.IsFried() && e.progressNormalized >= burnShowProgress;

   }

   private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
   {
      bool playSound = e.stateChanged == StoveCounter.State.Cooking
         || e.stateChanged == StoveCounter.State.Fried;

      if (playSound) audioSource.Play();
      else audioSource.Stop();
   }
}
