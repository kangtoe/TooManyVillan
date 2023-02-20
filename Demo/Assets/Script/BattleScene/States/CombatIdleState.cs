using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatIdleState : State<BaseCharacterController>
{
    public Animator animator;

    protected int hashIdle = Animator.StringToHash("Idle");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        animator?.SetBool(hashIdle, true);
    }

    public override void Update(float deltaTime)
    {
        if (BattleSceneManager.instance.isEngaging)
        {
            if (context.FallBack)
            {
                stateMachine.ChangeState<FallBackState>();
                return;
            }
            else
            {
                if (context.attackTarget)
                {
                    if (context.CanAttack)
                    {
                        stateMachine.ChangeState<AttackState>();
                        return;
                    }
                    return;
                }
                else
                {
                    stateMachine.ChangeState<CombatMoveState>();
                    return;
                }
            }
        }
        

        

    }

    public override void OnExit()
    {
        animator?.SetBool(hashIdle, false);
    }
}
