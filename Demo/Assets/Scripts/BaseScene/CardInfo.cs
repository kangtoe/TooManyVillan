using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new card", menuName = "Character")]
public class CardInfo : ScriptableObject
{
    public  Sprite profile_image;
    public int id;
    public  Characters characters;
}
   

