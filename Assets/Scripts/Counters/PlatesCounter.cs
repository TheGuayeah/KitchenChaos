using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
   public event EventHandler OnPlateSpawned;
   public event EventHandler OnPlateRemoved;

   [SerializeField]
   private float spawnTime = 4f;
   [SerializeField]
   private KitchenObjectSO plateSO;

   private float spawnPlateTimer;
   private int platesAmmount;
   private int maxPlates = 5;

   private void Update()
   {
      spawnPlateTimer += Time.deltaTime;
      if (spawnPlateTimer > spawnTime)
      {
         spawnPlateTimer = 0f;

         if (platesAmmount < maxPlates)
         {
            platesAmmount++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
         }
      }
   }

   public override void Interact(Player player)
   {
      if (!player.HasKitchenObject()) // Player has no object
      {
         if (platesAmmount > 0)
         {
            platesAmmount--;
            KitchenObject.SpawnKitchenObject(plateSO, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
         }
      }
   }
}
