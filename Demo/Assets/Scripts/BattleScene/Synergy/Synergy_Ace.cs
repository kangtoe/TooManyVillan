using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SynergyBase;

public class Synergy_Ace : SynergyBase
{
    protected override void Awake()
    {
        base.Awake();
        mAtivateTime = EActivateTime.Always;
        mType = ESynergyType.Ace;
    }
    public override void AddSynergyCount()
    {
        BuffManager.instance.count_Ace += 1;
    }
}
