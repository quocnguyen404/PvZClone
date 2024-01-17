using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : ProjectileShooter
{
    protected override void ShotProjectile()
    {
        AudioManager.Instance.PlaySound(Sound.PeaShoot);
        base.ShotProjectile();
    }
}
