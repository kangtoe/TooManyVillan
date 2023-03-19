using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehaviour_AreaAttack : AttackBehaviour
{
    public ManualCollision attackCollision;

    public override void ExecuteAttack(GameObject target = null, Transform startPoint = null, float attackMult = 1f)
    {
        
        Collider2D[] colliders = attackCollision?.CheckOverlapBox(targetMask);
        
        
        foreach (Collider2D collider in colliders)
        {
            
            collider.gameObject.GetComponent<IDamageable>()?.TakeDamage((int)(damage * attackMult), effectPrefab);
            collider.gameObject.GetComponent<BaseCharacterController>()?.KnockBack(knockBackForce);
        }

        calcCoolTime = 0.0f;
    }

}
