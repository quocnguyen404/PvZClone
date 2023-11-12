using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IProduct
{
    [SerializeField] protected Rigidbody rgBody = null;
    
    protected float speed;
    protected float damage;

    public void InitProjectile(Vector3 initPos)
    {
        transform.position = initPos;
        rgBody.velocity = Vector3.zero;
    }

    public void MoveToTarget(Vector3 dir)
    {
        rgBody.AddForce(dir * speed);
        ReturnPool(10f);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        IUnit unit = other.gameObject.GetComponent<Zombie>();

        if (unit != null)
        {
            unit.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
