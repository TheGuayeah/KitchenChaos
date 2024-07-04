using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
   [SerializeField]
   private BaseCounter baseCounter;
   [SerializeField]
   private GameObject[] visualSelectedObjs;

   private void Start()
   {
      Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
   }

   private void Player_OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
   {
      if (e.selectedCounterChanged == baseCounter)
      {
         Show();
      }
      else
      {
         Hide();
      }
   }

   private void Show()
   {
      foreach (GameObject visual in visualSelectedObjs)
      {
         visual.SetActive(true);
      }
   }

   private void Hide()
   {
      foreach (GameObject visual in visualSelectedObjs)
      {
         visual.SetActive(false);
      }
   }
}
