using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IProduct
{
    [SerializeField] protected Rigidbody rgBody = null;

    protected float speed;
    protected float damage;

    public float DebuffValue = 0f;
    public float DebuffDuration = 0f;
    public DebuffType Type = DebuffType.None;

    public void InitProjectile(Vector3 initPos, Data.UnitData unitData)
    {
        transform.position = initPos;
        rgBody.velocity = Vector3.zero;
        speed = unitData.attributes[(int)Data.AttributeType.SP].value;
        damage = unitData.attributes[(int)Data.AttributeType.ATK].value;
    }

    public void MoveToTarget(Vector3 dir)
    {
        rgBody.AddForce(dir * speed);
        ReturnPool(existTime);
    }

    protected override void ReturnPool()
    {
        base.ReturnPool();
        rgBody.velocity = Vector3.zero;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(GameConstant.PROJECTILE_BLOCK_TAG))
        {
            ReturnPool();
            return;
        }

        IUnit unit = other.gameObject.GetComponent<Zombie>();

        if (unit != null)
        {
            unit.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    protected virtual void Debuff(IUnit unit)
    {

    }
}
