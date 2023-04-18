using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_Saibi : SynergyBase
{
    // Start is called before the first frame update

    protected override void Awake() // 이 때 동작안하면, SetSynergy도 호출해야함
    {
        base.Awake();
        mAtivateTime = EActivateTime.Always;
        mType = ESynergyType.Saibi;
    }

    public override void AddSynergyCount()
    {
        BuffManager.instance.count_Saibi += 1;
    }
}
