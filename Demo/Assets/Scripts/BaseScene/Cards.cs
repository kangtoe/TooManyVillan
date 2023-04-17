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
   
    // Start is called before the first frame update
    void Start()
    {
        if(card != null)
        {
            img.sprite = card.image;
            name.text = DB.Villain[card.id].Vil_Name;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
