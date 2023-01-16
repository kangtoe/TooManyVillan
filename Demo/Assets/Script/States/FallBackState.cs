using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBackState : State<BaseCharacterController>
{
    public Animator animator;
    private int hashMove = Animator.StringToHash("Move");

    float moveDist; // 현재 도망 거리
    float startPosX; // 도망 시작 위치
    float moveTime = 0; // 도망 경과 시간

    public bool fallBackEnough => (context.fallBackDistance < moveDist) || (context.fallBackTime < moveTime);
    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        animator?.SetBool(hashMove, true);

        context.Flip(); // 플립
        startPosX= context.transform.position.x;
    }

    public override void Update(float deltaTime)
    {
        moveDist = Mathf.Abs(startPosX - context.transform.position.x); // 실시간 이동한 거리
        moveTime += deltaTime; // 실시간 이동 이후 경과 시간
        Transform tf = context.transform;
        tf.Translate(tf.right * context.dir * context.moveSpeed * Time.deltaTime * -1); // 뒤로 뛰어서 left로 바꾸었다
        // 적이 탐색 사거리 내에 있는 경우 - > 공격 사거리 내인지 아닌지 따라서 나눴다. 
        
        if (fallBackEnough)
        {
            if (context.attackTarget)
            {
                if (context.CanAttack)
                {
                    context.Flip();
                    stateMachine.ChangeState<AttackState>();
                    moveTime = 0;
                }
                else
                {
                    context.Flip();
                    stateMachine.ChangeState<CombatIdleState>();
                    moveTime = 0;
                }
            }
            else
            {
                context.Flip();
                stateMachine.ChangeState<CombatMoveState>();
                moveTime = 0;
            }

            return;
        }
       
    }

    public override void OnExit()
    {
        animator?.SetBool(hashMove, false);
    }

}
