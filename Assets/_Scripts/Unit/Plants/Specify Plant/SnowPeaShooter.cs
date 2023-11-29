using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPeaShooter : ProjectileShooter
{
    public override void Update()
    {
        base.Update();

        if (IsEndGame)
            return;
    }
}
