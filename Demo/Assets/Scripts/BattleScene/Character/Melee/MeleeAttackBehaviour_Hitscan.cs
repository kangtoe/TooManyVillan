using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehaviour_Hitscan : AttackBehaviour
{

    public override void ExecuteAttack(GameObject target = null, Transform startPoint = null, float attackMult = 1f)
    {
        target.GetComponent<IDamageable>()?.TakeDamage((int)(damage * attackMult), effectPrefab); // 쏘는 즉시 데미지가 들어가면 된다.

        calcCoolTime = 0.0f;
    }
}
