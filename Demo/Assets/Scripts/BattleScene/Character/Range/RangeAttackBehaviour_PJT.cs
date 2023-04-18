using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackBehaviour_PJT : AttackBehaviour // 원거리 캐릭터 발사체 공격
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    private float MultNum = 2.5f; // 기본적으로 공격 스킬에 곱해주는 mult 수치

    protected override void Awake()
    {
        base.Awake();

        defaultMult = MultNum;
        damage = (int)(damage * defaultMult);
    }

    public override void ExecuteAttack(GameObject target = null, Transform firePoint = null, float attackMult = 1)
    {
        Vector3 vec = transform.position;

        if (firePoint)
        {
            vec = firePoint.position;
        }

        GameObject go = Instantiate(bullet, vec, transform.rotation);

        go.GetComponent<Bullet>().shooter = this.GetComponent<BaseCharacterController>();

        go.GetComponent<Bullet>().SetDamage((int)(damage * attackMult));
        go.GetComponent<Bullet>().SetTarget(targetMask);

        calcCoolTime = 0.0f;
    }
}
