using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoveCounterSound : MonoBehaviour
{
   [SerializeField]
   private StoveCounter stoveCounter;

   private AudioSource audioSource;

   private void Awake()
   {
      audioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
   }

   private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
   {
      bool playSound = e.stateChanged == StoveCounter.State.Cooking
         || e.stateChanged == StoveCounter.State.Fried;

      if (playSound) audioSource.Play();
      else audioSource.Stop();
   }
}
