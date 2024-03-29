using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : UnitManager
{
    public Transform PoolTransform = null;
    public System.Func<int, int, Node> OnPlantGetNode = null;
    public System.Func<int, List<Node>> OnPlantGetPath = null;
    public System.Func<int, int, List<Node>> OnPlantGetArea = null;

    public override void AddUnit(IUnit unit)
    {
        base.AddUnit(unit);

        Plant plant = PUnitCast(unit);
        plant.PoolPosition = PoolTransform.position;
        plant.OnGetNode = PlantGetNode;
        plant.OnGetPath = PlantGetPath;
        plant.OnGetArea = PlantGetArea;
        plant.OnGetProduct = GetProduct;
        plant.OnGetSound = GetSound;
    }

    private Plant PUnitCast(IUnit unit)
    {
        Plant pUnit = unit as Plant;
        return pUnit;
    }

    private IProduct GetProduct(Data.UnitData data)
    {
        return OnUnitGetProduct?.Invoke(data);
    }



    private Node PlantGetNode(int row, int column)
    {
        return OnPlantGetNode?.Invoke(row, column);
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
