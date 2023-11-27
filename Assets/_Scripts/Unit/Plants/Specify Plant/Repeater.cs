using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : ProjectileShooter
{
    protected override void Attack()
    {
        ShotProjectile();

        DOVirtual.DelayedCall(0.4f, () =>
        {
            ShotProjectile();
        }).SetAutoKill();
    }
}
