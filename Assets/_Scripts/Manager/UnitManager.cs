using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<IUnit> units = null;
    public Dictionary<int, List<IUnit>> rows = null;

    public System.Action<IProduct> OnUnitGetProduct = null;

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
}
