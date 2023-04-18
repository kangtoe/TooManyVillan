using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMoveState : State<BaseCharacterController>
{
    public Animator animator;
 
    private int hashMove = Animator.StringToHash("Move");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        animator?.SetBool(hashMove, true);
    }

    public override void Update(float deltaTime)
    {
        // 적이 탐색 사거리 내에 있는 경우 - > 공격 사거리 내인지 아닌지 따라서 나눴다.
        if(BattleSceneManager.instance.isEngaging)
        {
            if (context.attackTarget)
            {
                if (context.CanAttack)
                {
                    //Debug.Log("AttackBehaviour CoolTIme : " + context.CurrentAttackBehaviour.calcCoolTime);
                    stateMachine.ChangeState<AttackState>();
                    return;
                }

                stateMachine.ChangeState<CombatIdleState>();
                return;
            }

            // 여기서도 engaging 인지를 판단해야 적이 없으면 

            Transform tf = context.transform;

            tf.Translate(tf.right * context.dir * context.moveSpeed * Time.deltaTime);
        }
    }

    public override void OnExit()
    {
        animator?.SetBool(hashMove, false);
    }

}
