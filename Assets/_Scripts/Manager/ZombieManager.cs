using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : UnitManager
{
    private Vector3 nodeBottom;

    public override void Initialize()
    {
        base.Initialize();

        float x = transform.position.x - ((5 * 1) / 2) + (1 / 2);
        float z = transform.position.z - ((5 * 1) / 2) + (1 / 2);
        nodeBottom = new Vector3(x, transform.position.y, z);
    }

    public void DispatcherZombie()
    {

    }

    public override void AddUnit(IUnit unit)
    {
        base.AddUnit(unit);

        unit.transform.position = RandomPositionOnPlane();
    }

    public Vector3 RandomPositionOnPlane()
    {
        int x = Random.Range(1, 5) * 1;
        int z = Random.Range(0, 5) * 1;

        return new Vector3(x + nodeBottom.x, transform.position.y, z + nodeBottom.z);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(nodeBottom, Vector3.one * 0.3f);
    }
}
