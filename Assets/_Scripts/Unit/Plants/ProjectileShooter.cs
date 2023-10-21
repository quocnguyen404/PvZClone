using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : Plant
{
    [SerializeField] protected Transform spawnPoint = null;

    public int maxRange = 8;
   
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
        

        if (Physics.Raycast(spawnPoint.position, spawnPoint.right, (float)maxRange, zombieLayerMask))
            value = true;

        return value;
    }
    
    protected virtual void Attack()
    {
        
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
