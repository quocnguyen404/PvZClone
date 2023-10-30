using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IProduct
{
    [SerializeField] private Rigidbody rgBody = null;

    [SerializeField] private float speed;
    [SerializeField] private float damage;

    public void InitProjectile(Vector3 initPos)
    {
        transform.position = initPos;
        rgBody.velocity = Vector3.zero;
    }

    public void MoveToTarget(Vector3 dir)
    {
        rgBody.AddForce(dir * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        IUnit unit = other.gameObject.GetComponent<Zombie>();

        if (unit != null)
        {
            unit.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
