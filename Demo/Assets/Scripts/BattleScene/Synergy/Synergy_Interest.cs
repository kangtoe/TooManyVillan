using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SynergyBase;

public class Synergy_Interest : SynergyBase
{
    protected override void Awake()
    {
        base.Awake();
        mAtivateTime = EActivateTime.Always;
        mType = ESynergyType.Interest;
    }
    public override void AddSynergyCount()
    {
        BuffManager.instance.count_Interest += 1;
    }
}
