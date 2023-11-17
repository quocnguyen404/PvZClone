using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : Plant
{
    [SerializeField] protected Transform spawnPoint = null;
    [SerializeField] protected int MaxRange;

    protected int maxRange = 0;
    protected float attackTimer = 0;

    protected Projectile projectTile = null;

    protected void OnDisable()
    {
        maxRange = 0;
    }

    public override void Update()
    {
        base.Update();

        if (!DetectEnemy())
            return;

        if (attackTimer <= 0)
        {
            Attack();
            attackTimer = UnitData.attributes[(int)Data.AttributeType.AAI].value;
        }

        attackTimer -= Time.deltaTime;
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        RangeCalculate(node);
    }

    protected virtual bool DetectEnemy()
    {
        if (maxRange == 0)
            return false;

        if (nodesPath == null)
            return false;

        bool value = false;

        Node checkNode = nodesPath.Find(n => n.HasZombie() && n.GridPosition.y < maxRange && n.GridPosition.y >= GridPosition.y);

        value = checkNode != null;

        return value;
    }

    protected virtual void Attack()
    {
        ShotProjectile();
    }

    protected virtual void ShotProjectile()
    {
        projectTile = (Projectile)(OnGetProduct?.Invoke(UnitData));

        projectTile.InitProjectile(spawnPoint.position, UnitData);

        Vector3 dir = transform.right;
        projectTile.MoveToTarget(dir);
    }

    protected virtual void RangeCalculate(Node node)
    {
        maxRange = MaxRange + node.GridPosition.y;

        if (maxRange > GameConstant.GARDEN_COLOUMN)
        {

        }
    }

    public override void Dead()
    {
        base.Dead();
        attackTimer = UnitData.attributes[(int)Data.AttributeType.AAI].value;
    }
}
