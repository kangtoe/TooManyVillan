using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    #region Variables

#if UNITY_EDITOR
    [Multiline]
    public string developmentDescription = "";
#endif // UNITY_EDITOR

    public int animationIndex;

    public int damage;
    
    public int priority;

    public float knockBackForce;

    [SerializeField]
    private float coolTime;

    [SerializeField]
    public float calcCoolTime = 0.0f;
    //protected float calcCoolTime = 0.0f;

    public GameObject effectPrefab;

    //[HideInInspector]

    public LayerMask targetMask;

    [SerializeField]
    public bool IsAvailable => calcCoolTime >= coolTime;

    #endregion Variables
    protected virtual void Start()
    {
        calcCoolTime = coolTime;
    }

    protected void Update()
    {
        if (calcCoolTime < coolTime) // 이부분은 맞게 짜야할듯
        {
            calcCoolTime += Time.deltaTime;
        }
    }

    public abstract void ExecuteAttack(GameObject target = null, Transform startPoint = null, float attackMult = 1f);


}
