using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : Plant
{
    [SerializeField] protected Transform spawnPoint = null;

    protected int maxRange = 0;
    protected float timer = 0;

    private Projectile projectTile = null;

    protected void OnDisable()
    {
        maxRange = 0;
    }

    protected virtual void Update()
    {
        if (!IsOnNode)
            return;

        if (!DetectEnemy())
            return;

        Attack();
    }

    public override void PlaceUnitOnNode(Node node)
    {
        base.PlaceUnitOnNode(node);
        RangeCalculate(node);
    }

    protected virtual bool DetectEnemy()
    {
        bool value = false;

        Node checkNode = nodesPath.Find(n => n.hasZombie && n.GridPosition.y <= maxRange && n.GridPosition.y >= GridPosition.y);

        value = checkNode != null;

        return value;
    }

    protected virtual void Attack()
    {
        if (timer == 0)
        {
            ShotProjectile();
        }

        timer += Time.deltaTime;

        if (timer >= UnitData.attributes[(int)Data.AttributeType.AAI].value)
            timer = 0;
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
        maxRange = GameConstant.GARDEN_COLOUMN - node.GridPosition.y;
    }

    public override void Dead()
    {
        base.Dead();
    }
}
