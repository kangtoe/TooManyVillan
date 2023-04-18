using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_NightVil : SynergyBase
{
    protected override void Awake()
    {
        base.Awake();
        mAtivateTime = EActivateTime.Night;
        mType = ESynergyType.NightVil;
    }

    public override void AddSynergyCount()
    {
        BuffManager.instance.count_NightVil += 1;
    }
}
