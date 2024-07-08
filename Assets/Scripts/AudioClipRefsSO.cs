using UnityEngine;

[CreateAssetMenu(fileName = "New AudioClipRefsSO", menuName = "SrictableObjects/AudioClipRefsSO", order = 0)]
public class AudioClipRefsSO : ScriptableObject
{
   public AudioClip stoveSizzle;
   public AudioClip[] chop;
   public AudioClip[] deliveryFail;
   public AudioClip[] deliverySuccess;
   public AudioClip[] footStep;
   public AudioClip[] objectDrop;
   public AudioClip[] objectPickUp;
   public AudioClip[] trash;
   public AudioClip[] warning;
}
