using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Employment : MonoBehaviour
{
    
    [SerializeField] private VillainDB villainDB;
    [SerializeField] private GatchaRate gatcha;
    [SerializeField] private Transform parent, pos;
    [SerializeField] private GameObject profileGO;
    
    GameObject profileCard;
    Cards card;

    private int total = 0;


    // private void Gatcha()
    // {
    //     if(profileCard == null)
    //     {
    //         profileCard = Instantiate(profileGO, pos.position, Quaternion.identity) as GameObject;
    //         profileCard.transform.SetParent(parent);
    //         profileCard.transform.localScale = new Vector3(1,1,1);
    //         profile = profileCard.GetComponent<Profiles>();

    //         int rnd = UnityEngine.Random.Range(1,101);
    //         for(int i = 0 ; i<gatcha.Length ; i++)
    //         {
    //             if(gatcha[i].rate <= rnd)
    //             {
    //                 profile.id = RewardID(gatcha[i].rarity);
    //                 return;
    //             }
    //         }
    //     }
    // }

    // int RewardID(string rarity)
    // {
    //     GatchaRate gr = Array.Find(gatcha, rt => rt.rarity == rarity);

    //     int rnd = UnityEngine.Random.Range(0,reward.Length);
    //     return reward[rnd];
    // }


}
