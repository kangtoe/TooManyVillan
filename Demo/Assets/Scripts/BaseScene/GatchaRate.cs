using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GatchaRate
{
   public string rarity;
   public Sprite frame;

   [Range(1,100)]
   public int rate;

   public CardInfo[] reward;
}
