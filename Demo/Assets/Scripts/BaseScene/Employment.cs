using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Employment : MonoBehaviour
{
    [SerializeField]
    private VillainDB villainDB;
    private int total = 0;


    private void Start()
    {
        for(int i = 0 ; i< villainDB.Villain.Count ; ++i){
            total += villainDB.Villain[i].weight;
        }
    }
}
