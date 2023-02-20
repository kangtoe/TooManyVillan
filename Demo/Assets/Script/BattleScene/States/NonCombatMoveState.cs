using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCombatMoveState : State<BaseCharacterController>
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
        if (BattleSceneManager.instance.isEngaging)
        {
            stateMachine.ChangeState<CombatIdleState>();
            return;
        }

        Transform tf = context.transform;

        tf.Translate(tf.right * context.dir * context.MyGroup.groupSpeed * Time.deltaTime);
     
    
    }

    public override void OnExit()
    {
        animator?.SetBool(hashMove, false);
    }
}
