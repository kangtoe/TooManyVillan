using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cards : MonoBehaviour
{
    public VillainDB DB;
    public CardInfo card;

    [SerializeField] public Image img;
    [SerializeField] private TextMeshProUGUI name;

    public Image frame;
    
    void Start()
    {
        Init();
        if(card != null)
        {
            img.sprite = card.profile_image;
            name.text = card.characters.Vil_Name;
        }
        
    }

    void Init()
    {
        card.characters.id = card.id;
        card.characters.Vil_Name = DB.Villain[card.id].Vil_Name;
        card.characters.Vil_Grade = DB.Villain[card.id].Vil_Grade;
        card.characters.Range_Normal = DB.Villain[card.id].Range_Normal;
        card.characters.Range_Escape = DB.Villain[card.id].Range_Escape;
        card.characters.Vil_Synergy_1 = DB.Villain[card.id].Vil_Synergy_1;
        card.characters.Vil_Synergy_2 = DB.Villain[card.id].Vil_Synergy_2;
        card.characters.Vil_Cooltime = DB.Villain[card.id].Vil_Cooltime;

        card.characters.Vil_Hp = Random.Range( DB.Villain[card.id].Vil_Hp_Min, DB.Villain[card.id].Vil_Hp_Max);
        card.characters.Vil_Str = Random.Range( DB.Villain[card.id].Vil_Str_Min, DB.Villain[card.id].Vil_Str_Max);
        
    }
}
