using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<IUnit> units = null;
    public Dictionary<int, List<IUnit>> rows = null;
    public System.Func<Data.UnitData, IProduct> OnUnitGetProduct = null;

    public virtual void Initialize()
    {
        units = new List<IUnit>();
        rows = new Dictionary<int, List<IUnit>>();
    }

    public virtual void AddUnit(IUnit unit)
    {
        units.Add(unit);
    }

    public virtual void RemoveUnit(IUnit unit)
    {
        units.Remove(unit);
        rows[unit.GridPosition.x].Remove(unit);
    }

    protected virtual void AddUnitToRow(IUnit unit, int row)
    {
        if (rows.ContainsKey(row))
            rows[row].Add(unit);
        else
            rows[row] = new List<IUnit> { unit };
    }
}
