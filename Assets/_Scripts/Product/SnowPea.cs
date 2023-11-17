using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPea : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
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
