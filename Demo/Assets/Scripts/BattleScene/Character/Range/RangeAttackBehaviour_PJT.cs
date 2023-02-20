using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackBehaviour_PJT : AttackBehaviour // 원거리 캐릭터 발사체 공격
{
    [SerializeField]
    GameObject bullet;
    
    public override void ExecuteAttack(GameObject target = null, Transform firePoint = null)
    {
        Vector3 vec = transform.position;

        if (firePoint)
        {
            vec = firePoint.position;
        }

        GameObject go = Instantiate(bullet, vec, transform.rotation);

        go.GetComponent<Bullet>().SetDamage(damage);
        go.GetComponent<Bullet>().SetTarget(targetMask);
        calcCoolTime = 0.0f;
    }


}
