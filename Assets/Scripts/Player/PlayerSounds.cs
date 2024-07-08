using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSounds : MonoBehaviour
{
   private Player player;
   private float footStepTimer;
   private float footStepTimerMax = 0.1f;

   private void Awake()
   {
      player = GetComponent<Player>();
   }

   private void Update()
   {
      footStepTimer -= Time.deltaTime;

      if (footStepTimer < 0f)
      {
         footStepTimer = footStepTimerMax;

         if (player.IsWalking())
         {
            SoundManager.Instance.PlayFootstepsSound(player.transform.position);
         }
      }
   }
}
