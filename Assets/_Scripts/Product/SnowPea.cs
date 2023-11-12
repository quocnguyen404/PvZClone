using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPea : Projectile, IDebuff
{

    protected override void OnTriggerEnter(Collider other)
    {
        IUnit unit = other.gameObject.GetComponent<Zombie>();

        if (unit != null)
        {
            unit.TakeDamage(damage);
            gameObject.SetActive(false);
        }

    }

    public void IDebuff(IUnit unit)
    {
        Zombie zombie = (Zombie)unit;
        
    }
}
