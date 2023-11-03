using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] protected MeshRenderer meshRd = null;

    public Vector2Int GridPosition;
    public Vector3 WorldPosition;
    private List<IUnit> units = null;

    public bool hasZombie => HasZombie();

    public void Initialize()
    {
        units = new List<IUnit>();
    }

    public void AddUnit(IUnit unit)
    {
        unit.GridPosition = GridPosition;
        units.Add(unit);
    }

    public void RemoveUnit(IUnit unit)
    {
        units.Remove(unit);
    }

    public bool HasPlant()
    {
        IUnit unit = units.Find(n => n is Plant);

        return unit != null;
    }

    private bool HasZombie()
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

    public void ChangeColor(Color color)
    {
        meshRd.material.color = color;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(WorldPosition, Vector3.one * 0.3f);
    }
}

