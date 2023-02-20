using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackBehaviour_Hitscan : AttackBehaviour // 원거리 캐릭터 히트 스캔 공격
{

    public override void ExecuteAttack(GameObject target = null, Transform startPoint = null)
    {
        target.GetComponent<IDamageable>()?.TakeDamage(damage, effectPrefab); // target은 attackTarget으로 받아온다.
        
        calcCoolTime = 0.0f;
    }
}
