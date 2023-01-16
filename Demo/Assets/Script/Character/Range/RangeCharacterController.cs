//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RangeCharacterController : BaseCharacterController, IAttackable, IDamageable
//{
//    #region Variables

//    public Transform hitTransform; // 무기에 맞았을 때 이펙트가 발생되는 위치
//    public int maxHealth = 100;
//    public int health;

//    [SerializeField]
//    private List<AttackBehaviour> attackBehaviours = new List<AttackBehaviour>(); // 가능한 공격 및 스킬을 담은 리스트

//    #endregion Variables

    

//    #region Unity Methods
//    protected override void Start()
//    {
//        base.Start();
        
//        stateMachine.AddState(new MoveState());
//        stateMachine.AddState(new AttackState());
//        stateMachine.AddState(new FallBackState());
//        //stateMachine.AddState(new DeadState());

//        health = maxHealth;
//        InitAttackBehaviour();
//    }

//    protected override void Update()
//    {
//        base.Update();

//        if (stateMachine.CurrentState.GetType() != typeof(AttackState))
//        {
//            CheckAttackBehaviour();
//        }
        
//    }

//    #endregion Unity Methods

//    #region Helper Methods


//    private void InitAttackBehaviour()
//    {
//        foreach (AttackBehaviour behaviour in attackBehaviours)
//        {
//            if (CurrentAttackBehaviour == null)
//            {
//                Debug.Log("bahaviour : " + behaviour.animationIndex);
//                CurrentAttackBehaviour = behaviour;
//            }

//            behaviour.targetMask = TargetMask;
//        }
//    }

//    private void CheckAttackBehaviour()
//    {
//        if (CurrentAttackBehaviour == null || !CurrentAttackBehaviour.IsAvailable)
//        {
//            CurrentAttackBehaviour = null;

//            foreach (AttackBehaviour behaviour in attackBehaviours)
//            {
//                if (behaviour.IsAvailable)
//                {
//                    if ((CurrentAttackBehaviour == null) ||
//                        (CurrentAttackBehaviour.priority < behaviour.priority))
//                    {
//                        CurrentAttackBehaviour = behaviour;
//                    }
//                }
//            }
//        }
//    }

//    #endregion Helper Methods

//    #region IAttackable interfaces

//    public AttackBehaviour CurrentAttackBehaviour
//    {
//        get;
//        private set;
//    }
//    public void OnExecuteAttack(int attackIndex)
//    {
//        if (CurrentAttackBehaviour != null && attackTarget != null)
//        {
//            Debug.Log(attackIndex);
//            CurrentAttackBehaviour.ExecuteAttack(attackTarget.gameObject, projectileTransform);
//        }
//    }

//    #endregion IAttackable interfaces

//    #region IDamageable interfaces

//    public bool IsAlive => health > 0;

//    public void TakeDamage(int damage, GameObject hittEffectPrefab)
//    {
//        if (!IsAlive)
//        { 
//            return;
//        }

//        health -= damage;

//        if (hittEffectPrefab)
//        {
//            Instantiate(hittEffectPrefab, hitTransform);
//        }

//        if (IsAlive)
//        {
//            //애니메이터를 추가해서 trigger 처리를 해서 맞는 애니메이션을 넣을 것인지, State를 만들 것인지?
//        }
//        else
//        {
//            //stateMachine.ChangeState<DeadState>();
//        }

//    }

//    #endregion IDamageable Interfaces


//    #region Other Methods
  
    

    

//    #endregion Other Methods
//}
