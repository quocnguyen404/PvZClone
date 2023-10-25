using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : Plant
{
    [SerializeField] protected Transform spawnPoint = null;

    private int maxRange = 0;
    private float timer = 0;

    private Projectile projectTile = null;
    System.Func<Projectile> OnGetProjectile = null;

    protected void OnDisable()
    {
        maxRange = 0;
    }

    protected virtual void Update()
    {
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
        if (maxRange == 0)
            return false;

        bool value = false;


        //if (Physics.Raycast(spawnPoint.position, spawnPoint.right, (float)maxRange, zombieLayerMask))
        //    value = true;

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
        projectTile = OnGetProjectile?.Invoke();

        projectTile.InitProjectile(spawnPoint.position);

        Vector3 dir = transform.right;
        projectTile.MoveToTarget(dir);
    }

    protected virtual void RangeCalculate(Node node)
    {
        maxRange = GameConstant.GARDEN_COLOUMN - node.GridPosition.y;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = spawnPoint.TransformDirection(Vector3.right) * maxRange;
        Gizmos.DrawRay(spawnPoint.position, direction);
    }

}
