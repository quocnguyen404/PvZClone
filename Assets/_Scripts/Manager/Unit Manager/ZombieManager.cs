using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : UnitManager
{
    [SerializeField] private GridManager gridManager = null;

    public System.Action OnZombieWin = null;
    public System.Action OnZombieDie = null;
    public System.Func<int, List<Node>> OnZombieGetPath = null;

    public override void Initialize()
    {
        base.Initialize();
        gridManager.isBold = false;
        gridManager.Initialize(GameConstant.ZOMBIE_ROW, GameConstant.ZOMBIE_COLUMN, GameConstant.NODE_LENGTH);
    }

    public void DispatcherZombie(string zombieName)
    {
        Zombie zombie = GetZombieAlive(zombieName);
        zombie.OnGetPath = ZombieGetPath;
        zombie.InitializeRow(zombie.GridPosition.x);
    }

    public override void AddUnit(IUnit unit)
    {
        int row = Random.Range(0, GameConstant.ZOMBIE_ROW);
        int column = Random.Range(3, GameConstant.ZOMBIE_COLUMN);

        base.AddUnit(unit);
        Zombie zombie = ZUnitCast(unit);
        zombie.OnZombieDie = ZombieDie;
        zombie.Initialize(new Vector2Int(row, column));
        zombie.transform.position = gridManager.GetRow(row)[column].WorldPosition;
    }

    public override void RemoveUnit(IUnit unit)
    {
        base.RemoveUnit(unit);
    }

    private List<Node> ZombieGetPath(int row)
    {
        List<Node> nodepaths = new List<Node>(OnZombieGetPath?.Invoke(row));
        nodepaths.AddRange(gridManager.GetRow(row));
        return nodepaths;
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
        return ZUnitCast(units.Find(z => ZUnitCast(z).IsAlive && !ZUnitCast(z).IsOnNode && z.UnitData.unitName == zombieName));
    }

    //public Vector3 RandomPositionOnPlane()
    //{
    //    int x = Random.Range(1, 5) * 1;
    //    int z = Random.Range(0, 5) * 1;

    //    return new Vector3(x + nodeBottom.x, transform.position.y, z + nodeBottom.z);
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(nodeBottom, Vector3.one * 0.3f);
    //}
}
