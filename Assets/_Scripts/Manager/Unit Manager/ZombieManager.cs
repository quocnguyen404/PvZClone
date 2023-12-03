using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : UnitManager
{
    [SerializeField] private Transform housePosition = null;

    public GridManager gridManager = null;

    public System.Action OnZombieWin = null;
    public System.Action OnZombieDie = null;
    public System.Func<int, List<Node>> OnZombieGetPath = null;

    public override void Initialize()
    {
        base.Initialize();
    }

    public void DispatcherZombie(string zombieName)
    {
        Zombie zombie = GetZombieAlive(zombieName);
        zombie.InitializeRow(zombie.GridPosition.x);
    }

    public override void AddUnit(IUnit unit)
    {
        int row = Random.Range(0, GameConstant.ZOMBIE_ROW);
        int column = Random.Range(GameConstant.GARDEN_COLOUMN + GameConstant.ZOMBIE_COLUMN - 3, GameConstant.GARDEN_COLOUMN + GameConstant.ZOMBIE_COLUMN);

        base.AddUnit(unit);
        Zombie zombie = ZUnitCast(unit);
        zombie.OnZombieDie = ZombieDie;
        zombie.OnGetPath = ZombieGetPath;
        zombie.OnGetHousePosition = ZombieGetHousePosition;
        zombie.OnZombieGetInHouse = ZombieGetInHouse;
        zombie.Initialize(new Vector2Int(row, column));
        zombie.transform.position = gridManager.GetRow(row)[column].WorldPosition;
        zombie.transform.eulerAngles = Helper.Cam.transform.eulerAngles;
    }

    public override void RemoveUnit(IUnit unit)
    {
        base.RemoveUnit(unit);
    }

    private List<Node> ZombieGetPath(int row)
    {
        List<Node> nodepaths = new List<Node>(OnZombieGetPath?.Invoke(row));
        return nodepaths;
    }

    private void ZombieDie()
    {
        OnZombieDie?.Invoke();
    }

    private void ZombieGetInHouse()
    {
        OnZombieWin?.Invoke();
    }

    private Vector3 ZombieGetHousePosition()
    {
        return housePosition.position;
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
}
