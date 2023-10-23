using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rgBody = null;

    public string projectileName = "";
    private float speed;
    private float damage;

    public void InitProjectile(Data.UnitData data, Vector3 initPos)
    {
        transform.position = initPos;
    }

    public void MoveToTarget(Vector3 dir)
    {
        rgBody.AddForce(dir * speed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        IUnit unit = collision.gameObject.GetComponent<Zombie>();

        if (unit != null)
        {
            unit.TakeDamage(damage);
        }
    }
}
