using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : UnitManager
{
    public System.Func<int, int, Node> OnGetNode = null;
    public System.Func<int, List<Node>> OnPlantGetPath = null;
    public System.Func<int, int, List<Node>> OnPlantGetArea = null;



    public override void AddUnit(IUnit unit)
    {
        base.AddUnit(unit);

        Plant plant = PUnitCast(unit);
        plant.OnGetPath = PlantGetPath;
        plant.OnGetArea = PlantGetArea;
        plant.OnGetProduct = GetProjectile;
    }

    private Plant PUnitCast(IUnit unit)
    {
        Plant pUnit = unit as Plant;
        return pUnit;
    }

    private IProduct GetProjectile(Data.UnitData data)
    {
        return OnUnitGetProduct?.Invoke(data);
    }

    private Node PlantGetNode(int row, int column)
    {

    }

    private List<Node> PlantGetPath(int row)
    {
        return OnPlantGetPath?.Invoke(row);
    }

    private List<Node> PlantGetArea(int row, int col)
    {
        return OnPlantGetArea?.Invoke(row, col);
    }

}
