using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllignState : State<BaseCharacterController>
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

        Transform tf = context.transform;

        if (tf.position.x > context.allignPoint)
        {
            context.Flip();
        }
    }

    public override void Update(float deltaTime)
    {
        Transform tf = context.transform;

        float dist = Mathf.Abs(tf.position.x - context.allignPoint); 

        if (tf.position.x > context.allignPoint) // 이동해야하는 곳보다 오른쪽에 있는 경우
        {
            
            tf.Translate(tf.right * context.dir * context.moveSpeed * Time.deltaTime * -1); //왼쪽으로 이동 

            if (dist < 0.01)
            {
                context.Flip();
                context.waitingTrigger = true;
                stateMachine.ChangeState<NonCombatIdleState>();
                return;
            }
        }
        else // 이동해야 하는 곳보다 왼쪽에 있는 경우
        {
            
            tf.Translate(tf.right * context.dir * context.moveSpeed * Time.deltaTime); //오른쪽으로 이동

            if(dist < 0.01)
            {
                context.waitingTrigger = true;
                stateMachine.ChangeState<NonCombatIdleState>();
                return;
            }
        }

        
    }

    public override void OnExit()
    {
        animator?.SetBool(hashMove, false);
    }
}
