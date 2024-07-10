using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
   private const string PLAYER_PREFS_SFX_VOLUME = "SFX_Volume";

   [SerializeField]
   private AudioClipRefsSO audioClipRefsSO;

   private float volume = 1f;

   private new void Awake()
   {
      base.Awake();
      volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
   }

   private void Start()
   {
      DeliveryManager.Instance.OnCommandSucceded += DeliveryManager_OnCommandSucceded;
      DeliveryManager.Instance.OnCommandFailed += DeliveryManager_OnCommandFailed;
      CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
      Player.Instance.OnPickedSomething += Player_OnPickedSomething;
      BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
      TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
   }

   private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
   {
      TrashCounter trashCounter = sender as TrashCounter;
      PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
   }

   private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
   {
      BaseCounter baseCounter = sender as BaseCounter;
      PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
   }

   private void Player_OnPickedSomething(object sender, System.EventArgs e)
   {
      Player player = sender as Player;
      PlaySound(audioClipRefsSO.objectPickUp, player.transform.position);
   }

   private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
   {
      CuttingCounter cuttingCounter = sender as CuttingCounter;
      PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
   }

   private void DeliveryManager_OnCommandFailed(object sender, System.EventArgs e)
   {
      DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
      PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
   }

   private void DeliveryManager_OnCommandSucceded(object sender, System.EventArgs e)
   {
      DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
      PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
   }
   private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1f)
   {
      AudioClip audioClip = audioClips[Random.Range(0, audioClips.Length)];
      AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
   }

   private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
   {
      AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
   }

   public void PlayFootstepsSound(Vector3 position, float volumeMultiplier = 1f)
   {
      PlaySound(audioClipRefsSO.footStep, position, volumeMultiplier * volume);
   }

   public void ChangeVolume(float newVolume)
   {
      volume = newVolume;
      PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, newVolume);
      PlayerPrefs.Save();
   }

   public float GetVolume()
   {
      return volume;
   }
}
