using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] protected MeshRenderer meshRd = null;

    public Vector2Int GridPosition;
    public Vector3 WorldPosition;
    private List<IUnit> units = null;

    public void Initialize()
    {
        units = new List<IUnit>();
    }

    public void AddUnit(IUnit unit)
    {
        unit.GridPosition = GridPosition;
        units.Add(unit);
        Debug.Log($"Add {unit.UnitData.unitName} to node {GridPosition.x}, {GridPosition.y}");
    }

    public void RemoveUnit(IUnit unit)
    {
        units.Remove(unit);
        Debug.Log($"Remove {unit.UnitData.unitName} to node {GridPosition.x}, {GridPosition.y}");
    }

    public bool HasPlant()
    {
        IUnit unit = units.Find(n => n is Plant);

        return unit != null;
    }

    public bool HasZombie()
    {
        IUnit unit = units.Find(n => n is Zombie && n.IsAlive);

        return unit != null;
    }

    public Plant GetPlantFromNode()
    {
        if (units == null)
            return null;

        Plant plant = units.Find(n => n is Plant) as Plant;

        return plant;
    }

    public List<IUnit> GetAllZobmie()
    {
        return units.FindAll(n => n is Zombie);
    }

    public void ChangeColor(Color color)
    {
        meshRd.material.color = color;
    }
}

