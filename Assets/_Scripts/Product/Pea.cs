using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : Projectile
{
    private void Awake()
    {
        hitSound = Sound.PeaHit;
    }
}
