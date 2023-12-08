using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puffshroom : ProjectileShooter, INightPlant
{
    public NightPlantComposite NightPlant
    {
        get;
        set;
    }

    public override void Initialize(Vector2Int pos)
    {
        base.Initialize(pos);
        NightPlant.Initialize(ator);
    }

    public override void Update()
    {
        if (NightPlant.IsSleeping)
            return;

        base.Update();
    }
}
