using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_Newbie : SynergyBase
{
    public override void AddSynergyCount()
    {
        BuffManager.instance.count_Newbie += 1;
    }
}
