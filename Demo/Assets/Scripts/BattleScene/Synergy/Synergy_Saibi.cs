using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_Saibi : SynergyBase
{
    // Start is called before the first frame update
    public override void AddSynergyCount()
    {
        BuffManager.instance.count_Saibi += 1;
    }
}
