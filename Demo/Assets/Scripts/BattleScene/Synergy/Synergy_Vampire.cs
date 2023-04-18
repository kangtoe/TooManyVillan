using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_Vampire : SynergyBase
{
    protected override void Awake()
    {
        base.Awake();
       
    }

    public override void AddSynergyCount()
    {
        mAtivateTime = EActivateTime.Always;
        mType = ESynergyType.Vampire;
        BuffManager.instance.count_Vampire += 1;
    }
}
