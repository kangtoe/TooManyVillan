using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State<BaseCharacterController>
{
    private Animator animator;

    protected int isAliveHash = Animator.StringToHash("IsAlive");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        animator?.SetBool(isAliveHash, false);
        context.MyGroup.RemoveCharacterInGroup(context);
    }

    public override void Update(float deltaTime)
    {
        if (stateMachine.ElapsedTimeInState > 1.0f)
        {
            GameObject.Destroy(context.gameObject);
        }
    }

    public override void OnExit()
    {
    }
}
