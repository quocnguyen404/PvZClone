using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class UnitManager : MonoBehaviour
{
    public List<IUnit> units = null;
    public Dictionary<int, List<IUnit>> rows = null;
    public System.Func<Data.UnitData, IProduct> OnUnitGetProduct = null;
    public System.Func<Sound, AudioClip> OnUnitGetSound = null;
    public virtual void Initialize()
    {
        units = new List<IUnit>();
        rows = new Dictionary<int, List<IUnit>>();
    }

    public virtual void AddUnit(IUnit unit)
    {
        units.Add(unit);
        unit.PoolPosition = this.transform.position;
    }

    public virtual void RemoveUnit(IUnit unit)
    {
        units.Remove(unit);
        rows[unit.GridPosition.x].Remove(unit);
    }

    protected virtual AudioClip GetSound(Sound soundType)
    {
        return OnUnitGetSound?.Invoke(soundType);
    }
}
