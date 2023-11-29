using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunProducing : Plant
{
    [SerializeField] private Transform spawnPoint = null;

    protected Sun sunTemp = null;

    protected float sunTimer = 0;

    public override void Update()
    {
        if (IsEndGame)
            return;

        if (!IsOnNode)
            return;

        ProduceSun();
    }

    protected virtual void ProduceSun()
    {
        if (sunTimer >= UnitData.attributes[(int)Data.AttributeType.AAI].value)
        {
            sunTemp = (Sun)OnGetProduct?.Invoke(UnitData);
            sunTemp.Initialize(spawnPoint.position, (int)UnitData.attributes[(int)Data.AttributeType.ATK].value);
            sunTemp.TossSun(transform.position);
            sunTemp = null;
            sunTimer = 0;
        }

        sunTimer += Time.deltaTime;
    }

    public override void Dead()
    {
        base.Dead();
        sunTimer = 0;
    }
}
