using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : ProjectileShooter
{

    public override void Update()
    {
        base.Update();

        if (IsEndGame)
            return;
    }

    protected override void Attack()
    {
        ShotProjectile();

        DOVirtual.DelayedCall(0.4f, () =>
        {
            ShotProjectile();
        }).SetAutoKill();
    }
}
