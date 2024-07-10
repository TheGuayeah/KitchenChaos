using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{
   private const string PLAYER_PREFS_MUSIC_VOLUME = "Music_Volume";

   private AudioSource audioSource;
   private float volume = 0.3f;

   private new void Awake()
   {
      base.Awake();
      audioSource = GetComponent<AudioSource>();
      volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);

      audioSource.volume = volume;
   }

   public void ChangeVolume(float newVolume)
   {
      volume = newVolume;
      PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, newVolume);
      PlayerPrefs.Save();

      audioSource.volume = volume;
   }

   public float GetVolume()
   {
      return volume;
   }
}
