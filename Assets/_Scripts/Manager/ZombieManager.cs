using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : UnitManager
{
    private Vector3 nodeBottom;

    public System.Action OnZombieWin = null;
    public System.Action OnZombieDie = null;
    public System.Func<int, List<Node>> OnZombieGetPath = null;

    public override void Initialize()
    {
        base.Initialize();

        float x = transform.position.x - ((5 * 1) / 2) + (1 / 2);
        float z = transform.position.z - ((5 * 1) / 2) + (1 / 2);
        nodeBottom = new Vector3(x, transform.position.y, z);
    }

    public void DispatcherZombie(int amount, string zombieName)
    {
        for (int i = 0; i < amount; i++)
        {
            int row = Random.Range(0, GameConstant.GARDEN_ROW);

            Zombie zombie = GetZombieAlive(zombieName);
            zombie.isOnNode = true;
            zombie.OnGetPath = ZombieGetPath;
            zombie.InitializeRow(row);
        }
    }

    public override void AddUnit(IUnit unit)
    {
        base.AddUnit(unit);
        unit.transform.position = RandomPositionOnPlane();
        Zombie zombie = ZUnitCast(unit);
        zombie.OnZombieDie = ZombieDie;
        zombie.isAlive = true;
    }

    public override void RemoveUnit(IUnit unit)
    {
        base.RemoveUnit(unit);
    }

    private List<Node> ZombieGetPath(int row)
    {
        return OnZombieGetPath?.Invoke(row);
    }

    private void ZombieDie()
    {
        OnZombieDie?.Invoke();
    }

    private Zombie ZUnitCast(IUnit unit)
    {
        Zombie zUnit = unit as Zombie;
        return zUnit;
    }

    private Zombie GetZombieAlive(string zombieName)
    {
        return ZUnitCast(units.Find(z => ZUnitCast(z).isAlive && !ZUnitCast(z).isOnNode && z.UnitData.unitName == zombieName));
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
