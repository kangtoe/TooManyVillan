using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<BaseCharacterController>
{
    private Animator animator;
    private AttackStateController attackStateController;
    private IAttackable attackable;

    protected int attackTriggerHash = Animator.StringToHash("AttackTrigger");
    protected int attackIndexHash = Animator.StringToHash("AttackIndex");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        attackStateController = context.GetComponent<AttackStateController>();
        attackable = context.GetComponent<IAttackable>();
     
    }

    public override void OnEnter()
    {
        //if (!context.CanAttack) // 공격할 대상이 없으면, 그리고 여기에 강의처럼 if문에 조건을 더 추가를 해야할지?
        //{
        //    Debug.Log("ChangeState<IdleState>()");
        //    stateMachine.ChangeState<IdleState>();
        //    return;
        //}

        attackStateController.enterAttackStateHandler += OnEnterAttackState;
        attackStateController.exitAttackStateHandler += OnExitAttackState;
        
        
        if (attackable.CurrentAttackBehaviour == null)
        {
            Debug.Log("CurrentAttackBehaviour == null");
            stateMachine.ChangeState<CombatIdleState>();
            return;
        }

        
        animator?.SetInteger(attackIndexHash, attackable.CurrentAttackBehaviour.animationIndex);
        animator?.SetTrigger(attackTriggerHash);


    }

    public void OnEnterAttackState()
    {
        //Debug.Log("OnEnterAttackState()");
    }

    public void OnExitAttackState()
    {
        //Debug.Log("OnExitAttackState");
        stateMachine.ChangeState<CombatIdleState>();
    }


    public override void Update(float deltaTime)
    {
        if(!context.IsAlive) // 공격이 다 끝나고 죽는 것이 아닌, 공격하다가도 죽을 수 있게 해야한다.
        {
            stateMachine.ChangeState<DeadState>();
        }

        //if (context.attackTarget) // 공격 대상이 있는 경우
        //{
        //    if (context.FallBack && attackStateController.IsInAttackState == false) // FallBack이며 StateMachine에서 나올 경우
        //    {
        //        stateMachine.ChangeState<FallBackState>();
        //        return;
        //    }
        //    else if (!context.FallBack && !context.CanAttack) // Fallback이 아니며, 공격 가능한 behaviour가 없으면 Idle로 이동
        //    {
        //        {
        //            stateMachine.ChangeState<IdleState>();
        //            return;
        //        }
        //    }
        //}
        //else if (!context.attackTarget)// 공격 대상이 없는 경우 -> 나중에 여기에 전투 중이면 Move로, 전투 종료면 Idle로 이동하게 만든다.
        //{
        //    stateMachine.ChangeState<MoveState>();
        //    return;
        //}
    }


    /*&& animator.GetCurrentAnimatorStateInfo(0).IsName("Toko_Attack1")
     && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // 도망 사거리에 들어왔고, 공격 애니메이션이 완료가 되면 State가 전환*/



    public override void OnExit()
    {
        attackStateController.enterAttackStateHandler -= OnEnterAttackState;
        attackStateController.exitAttackStateHandler -= OnExitAttackState;
        
       
    }

    //UnityEngine.Debug.Log("OnExitAttackState()");
    

}
