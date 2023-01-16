using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour, IAttackable, IDamageable// BaseCharacterController에는 적 탐색을 위해서, 그러기 위해선 거리는 여기에 둬야 한다. 
{
    
   //IAttackable 공격이 가능한 캐릭터인지, IDamageable 데미지를 받는 캐릭터인지
    #region Variables

    protected StateMachine<BaseCharacterController> stateMachine;

    public int index; // 캐릭터들 정렬 시 위치 인덱스
    public bool allignTrigger = false;
    public bool waitingTrigger = false; // 모두 정렬할 때까지 기다리고 있는가 
    public bool waitingToMoveTrigger = false; // 모두 정렬하고 이동할 수 있는가
    public float allignPoint; // 캐릭터 정렬 시 이동하는 x좌표 위치
    public GroupObject MyGroup; // 캐릭터 자신이 속해있는 그룹
    public GameObject hitEffectPrefab; 
    
    public int maxHealth = 100;
    public int health;

    public int dir = 1; // 1: right, -1 : left
    public LayerMask TargetMask;
    public Transform rayPoint; // RayCast 쏘는 위치
    List<Transform> targetsInRange = new List<Transform>(); // 공격 사거리 내에 있는 모든 적
    public Transform attackTarget; // 공격할 적
    public float attackRange; // 유닛 공격 사거리 - 고정
    public float moveSpeed = 1.0f;

    public float fallBackCheckDistance; // 유닛 도망 체크 거리
    public float fallBackDistance; // 유닛 실제 도망 거리
    public float fallBackTime; // 유닛 실제 도망 최대 시간
    public Transform projectileTransform; // 원거리 유닛 공격 시 발사체의 격발 위치


    [SerializeField]
    private List<AttackBehaviour> attackBehaviours = new List<AttackBehaviour>(); // 가능한 공격 및 스킬을 담은 리스트
    #endregion Variables

    #region Properties
    public bool FallBack => FallBackCheck();
    public bool CanAttack => CheckAttackBehaviour();// 현재 가능한 공격이 있는가? 
    //public bool IsCurrentAttackBehaviourAvailable => (CurrentAttackBehaviour != null) && ();
    #endregion Properties



    #region Unity Methods
    private void Start()
    {
        stateMachine = new StateMachine<BaseCharacterController>(this, new NonCombatMoveState());
        stateMachine.AddState(new AllignState());
        stateMachine.AddState(new NonCombatIdleState());

        stateMachine.AddState(new CombatIdleState());
        stateMachine.AddState(new CombatMoveState());
        stateMachine.AddState(new AttackState());
        stateMachine.AddState(new FallBackState());
        stateMachine.AddState(new DeadState());

       
        health = maxHealth;
        InitAttackBehaviour();

        
    }

    private void Update()
    {
        InAttackRangeCheck();
        AllignCheck();
        WaitingCheck();
        stateMachine.Update(Time.deltaTime);
        attackTarget = FindAttackTarget();

        
    }

    #endregion Unity Methods

    #region Other Methods

    //  사거리 내 모든 적들 검사 
    void InAttackRangeCheck()
    {
        if (targetsInRange != null)
        {
            targetsInRange.Clear();
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPoint.position, Vector2.right * dir, attackRange, TargetMask);  // 탐색 범위 내에 있는 타겟들을 반환 ex) 맵 절반
        //Debug.Log("Hits Length : " + hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            targetsInRange.Add(hits[i].transform);
        }
    }

    // attackTarget에 해당하는 Transform 반환(기본 : 가장 가까운 적을 탐색)
    Transform FindAttackTarget()
    {
        return FindClosestTarget();
    }

    // attackTarget 중에서 가장 가까운 적의 Transform 반환
    Transform FindClosestTarget()
    {
        if (targetsInRange == null || targetsInRange.Count == 0)
        {
            //Debug.Log("FindTarget fail");
            return null;
        }

        float minDist = Vector2.Distance(targetsInRange[0].position, transform.position);

        int idx = 0;

        for (int i = 1; i < targetsInRange.Count; i++)
        {
            float dist = Vector2.Distance(targetsInRange[0].position, transform.position);
            if (dist < minDist)
            {
                idx = i;
                minDist = dist;
            }
        }
        return targetsInRange[idx];
    }

    bool FallBackCheck() // 도망가야할지 검사
    {
        Transform tf = FindClosestTarget(); 
        if (tf == null) // 타겟이 없으면 거짓 반환
        {
            return false;
        }
        else // 타겟이 있으면 
        {
            float dist = Vector2.Distance(tf.position, transform.position);
            return dist <= fallBackCheckDistance;
        }


    }

    public void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }

    private void AllignCheck()
    {
        if (allignTrigger == true)
        {
            allignTrigger = false;

            stateMachine.ChangeState<AllignState>();

        }

    }

    private void WaitingCheck()
    {
        if(waitingToMoveTrigger == true)
        {
            waitingTrigger = false;
            waitingToMoveTrigger = false;

            stateMachine.ChangeState<NonCombatMoveState>();

        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "enemyRespawn")
    //    {
    //        BattleSceneManager.instance.CreateEnemy();
    //    }
    //}

    private void OnDrawGizmos() // 중요
    {
        Gizmos.color = Color.red;
        Vector3 direction = Vector3.right * dir * attackRange;
        Gizmos.DrawRay(transform.position, direction);

        Gizmos.color = Color.blue;
        Vector3 direction1 = Vector3.right * dir * fallBackCheckDistance;
        Gizmos.DrawRay(transform.position, direction1);

        Gizmos.color = Color.black;
        Vector3 direction2 = Vector3.right * dir * fallBackDistance;
        Gizmos.DrawRay(transform.position, direction2);
    }

    #endregion

    #region Helper Methods


    private void InitAttackBehaviour()
    {
        foreach (AttackBehaviour behaviour in attackBehaviours)
        {
            if (CurrentAttackBehaviour == null)
            {
                //Debug.Log("bahaviour : " + behaviour.animationIndex);
                CurrentAttackBehaviour = behaviour;
            }

            behaviour.targetMask = TargetMask;

            
        }
    }

    private bool CheckAttackBehaviour()
    {

        CurrentAttackBehaviour = null;

        foreach (AttackBehaviour behaviour in attackBehaviours)
        {

            if (behaviour.IsAvailable)
            {
                if ((CurrentAttackBehaviour == null) ||
                    (CurrentAttackBehaviour.priority < behaviour.priority))
                {
                    
                    CurrentAttackBehaviour = behaviour;
                    //Debug.Log("CurrentAttackBehaviour.priority  = " + CurrentAttackBehaviour.priority);
                }
            }

        }

        return CurrentAttackBehaviour; 

    }

    #endregion Helper Methods

    #region IAttackable interfaces

    public AttackBehaviour CurrentAttackBehaviour
    {
        get;
        private set;
    }
    public void OnExecuteAttack(int attackIndex)
    {
        if (CurrentAttackBehaviour != null && attackTarget != null)
        {
            //Debug.Log("OnExecuteAttack : " + attackIndex);
            CurrentAttackBehaviour.ExecuteAttack(attackTarget.gameObject, projectileTransform);
        }
    }

    #endregion IAttackable interfaces

    #region IDamageable interfaces

    public bool IsAlive => health > 0;

    public void TakeDamage(int damage, GameObject hittEffectPrefab)
    {
        if (!IsAlive)
        {
            return;
        }

        health -= damage;

        StartCoroutine("GetDamage");


        if (IsAlive)
        {
            //애니메이터를 추가해서 trigger 처리를 해서 맞는 애니메이션을 넣을 것인지, State를 만들 것인지?
        }
        else
        {
            stateMachine.ChangeState<DeadState>();
        }

    }

    IEnumerator GetDamage() // 데미지를 받을 때마다 캐릭터가 빨간색으로 깜빡거림
    {

        float blinkTime = 0.1f;
        int blinkCount = 2;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        for (int i = 0; i < blinkCount; i++)
        {
            sprite.color = Color.red;

            yield return new WaitForSeconds(blinkTime);

            sprite.color = Color.white;

            yield return new WaitForSeconds(blinkTime);
        }

    }
    #endregion IDamageable Interfaces


}

