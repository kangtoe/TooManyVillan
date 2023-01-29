using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask TargetMask;

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
