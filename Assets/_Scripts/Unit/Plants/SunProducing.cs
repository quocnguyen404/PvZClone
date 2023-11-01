using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunProducing : Plant
{
    [SerializeField] private Transform spawnPoint = null;

    public Sun sun = null;

    protected float timer = 0;

    protected virtual void Update()
    {
        if (IsOnNode)
        {
            ProduceSun();
        }
    }

    protected virtual void ProduceSun()
    {
        timer += Time.deltaTime;

        if (timer >= UnitData.attributes[(int)Data.AttributeType.AAI].value)
        {
            sun = (Sun)OnGetProduct?.Invoke(UnitData);
            sun.Initialize(spawnPoint.position, 50);
            sun.TossSun(transform.position);

            timer = 0;
        }

    }
}
