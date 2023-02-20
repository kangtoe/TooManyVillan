using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCombatIdleState : State<BaseCharacterController>
{
    public Animator animator;

    private int hashIdle = Animator.StringToHash("Idle");
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
        //if (context.MyGroup.AllCharactersWaiting) // 모두 대기중이면 이동, 여기서 다같이 적용이 안되고 하나만 된다.
        //{
        //    stateMachine.ChangeState<NonCombatMoveState>(); 
        //    return;
        //}

    }
    public override void OnExit()
    {
        animator?.SetBool(hashIdle, false);
    }

}
