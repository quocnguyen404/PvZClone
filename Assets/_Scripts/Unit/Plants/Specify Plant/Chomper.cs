using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Plant
{
    [SerializeField] protected int maxRange;

    protected float attackTimer = 0;

    public bool IsChewing
    {
        get
        {
            return attackTimer > 0;
        }
    }

    public override void Update()
    {
        if (IsEndGame)
            return;

        if (!IsOnNode)
            return;

        attackTimer -= Time.deltaTime;



        if (DetectEnemy() && !IsChewing)
        {
            Attack();
            return;
        }

        if (!IsChewing)
            ator.SetPlantMove(UnitAnimator.PlantStateType.Idle);
    }

    protected bool DetectEnemy()
    {
        bool value = false;

        value = nodesPath[GridPosition.y].HasZombie()
            || (nodesPath[GridPosition.y + maxRange].HasZombie()
            && Vector3.Distance(transform.position, nodesPath[GridPosition.y + maxRange].GetZombieFromNode().transform.position) <= (3f / 2f) * GameConstant.NODE_LENGTH);

        return value;
    }

    protected void Attack()
    {
        if (attackTimer <= 0)
        {
            Zombie zombie = null;

            for (int i = maxRange + GridPosition.y; i <= GridPosition.y; i--)
            {
                if (nodesPath[i].HasZombie())
                    zombie = nodesPath[i].GetZombieFromNode();
            }

            zombie = nodesPath[GridPosition.y + 1].GetZombieFromNode();

            ator.SetPlantMove(UnitAnimator.PlantStateType.Idle);

            if (zombie == null)
                return;

            ator.SetPlantMove(UnitAnimator.PlantStateType.Attack, () =>
            {
                zombie.GetBite(UnitData.attributes[(int)Data.AttributeType.ATK].value);

                if (!zombie.IsAlive)
                    ator.SetPlantMove(UnitAnimator.PlantStateType.Idle1);
                else if (zombie.IsAlive)
                    ator.SetPlantMove(UnitAnimator.PlantStateType.Idle);
            });

            attackTimer = UnitData.attributes[(int)Data.AttributeType.AAI].value;
        }

    }

}
