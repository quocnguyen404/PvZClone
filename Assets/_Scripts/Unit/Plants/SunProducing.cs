using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunProducing : Plant
{
    [SerializeField] private Transform spawnPoint = null;

    protected Sun sunTemp = null;

    protected float timer = 0;

    protected virtual void Update()
    {
        if (!IsOnNode)
            return;

        ProduceSun();
    }

    protected virtual void ProduceSun()
    {
        timer += Time.deltaTime;

        if (timer >= UnitData.attributes[(int)Data.AttributeType.AAI].value)
        {
            sunTemp = (Sun)OnGetProduct?.Invoke(UnitData);
            sunTemp.Initialize(spawnPoint.position, (int)UnitData.attributes[(int)Data.AttributeType.ATK].value);
            sunTemp.TossSun(transform.position);
            sunTemp = null;
            timer = 0;
        }
    }

    public override void Dead()
    {
        base.Dead();
        timer = 0;
    }
}
