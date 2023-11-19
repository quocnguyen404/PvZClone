using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Plant
{
    [SerializeField] protected int maxRange;

    protected float timer = 0;

    public bool IsChewing
    {
        get
        {
            return timer != 0;
        }
    }

    //public float CoolDown
    //{
    //    get
    //    {
    //    }
    //}


    protected void Update()
    {
        if (!IsOnNode)
            return;

        if (DetectEnemy())
            Attack();
    }

    protected bool DetectEnemy()
    {
        bool value = false;

        value = nodesPath[GridPosition.y].HasZombie() || nodesPath[GridPosition.y + 1].HasZombie();

        return value;
    }

    protected void Attack()
    {
        if (timer == 0)
        {
            Zombie zombie = null;

            for (int i = maxRange - 1; i >= 0; i++)
            {
                if (nodesPath[GridPosition.y + i].HasZombie())
                    zombie = nodesPath[GridPosition.y + i].GetZombieFromNode();
            }

        }

        timer += Time.deltaTime;

        if (timer >= UnitData.attributes[(int)Data.AttributeType.AAI].value)
            timer = 0;
    }

}
