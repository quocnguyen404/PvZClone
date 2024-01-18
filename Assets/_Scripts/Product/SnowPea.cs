using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPea : Projectile
{
    private void Awake()
    {
        hitSound = Sound.SnowPeaHit;
    }
    protected override void OnTriggerEnter(Collider other)
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
            Debuff(unit);
            gameObject.SetActive(false);
        }

    }

    protected override void Debuff(IUnit unit)
    {
        Zombie zombie = (Zombie)unit;

        zombie.TakeDebuff(DebuffDuration, Type, DebuffValue);
    }
}
