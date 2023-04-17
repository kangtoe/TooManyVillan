using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GatchaManager : MonoBehaviour
{
    [SerializeField] private GatchaRate[] gatcha;
    [SerializeField] private Transform parent, pos;
    [SerializeField] private GameObject characterCardGO;

    GameObject characterCard;
    Cards card;

    public int time;

    public void Gatcha()
    {
       for(int idx = 0 ; idx < time ; idx++)
       {
        characterCard = Instantiate(characterCardGO, pos.position + new Vector3(200*idx , 0 , 0), Quaternion.identity) as GameObject;
        characterCard.transform.SetParent(parent);
        characterCard.transform.localScale = new Vector3(1,1,1);
        card = characterCard.GetComponent<Cards>();
        
        int rnd = Random.Range(1,100);
        int adj = 0;
        for(int i = 0 ; i < gatcha.Length ; i++)
        {
            if(idx == time -1 && i == 1)    adj = 89;
            if(rnd <= gatcha[i].rate+adj)
            {
                card.card = Reward(gatcha[i].rarity);
                break;
            }
        } 
       }
       return;
    }

    CardInfo Reward(string rarity)
    {
        GatchaRate gr = Array.Find(gatcha , rt => rt.rarity == rarity);
        CardInfo[] reward = gr.reward;

        int rnd = UnityEngine.Random.Range(0,reward.Length);

        return reward[rnd];
    }

}
