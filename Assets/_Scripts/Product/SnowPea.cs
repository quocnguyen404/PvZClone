using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPea : Projectile, IDebuff
{
    [SerializeField] protected float debuffValue;

    public float DebuffValue 
    {
        get;
        set; 
    }

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

        if (zombie.isDebuff)
        {
            zombie.isDebuff = true;
            return;
        }

        //do debug
        zombie.isDebuff = true;
    }
}
