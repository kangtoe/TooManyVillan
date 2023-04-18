using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask TargetMask;

    public BaseCharacterController shooter;

    [SerializeField]
    float speed = 1;

    int damage = 1;

    [SerializeField]
    GameObject hitFx;


    void Update()
    {

        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.transform.gameObject.layer == TargetMask)
        {
            //Debug.Log("collision.gameObject.layer : " + collision.gameObject.layer);
            collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage, hitFx);
            Destroy(this.gameObject);


            if (shooter != null && shooter.isVampire) // 캐릭터가 Null이 아니며, Vampire인 경우
            {
                shooter.health += (int)(damage * 0.2f); // 가한 데미지의 20퍼센트만큼씩 회복
            }
        }


    }

    public void SetDamage(int i)
    {
        damage = i;
    }

    public void SetTarget(LayerMask layerMask) // 
    {
        TargetMask = layerMask;
    }
}
