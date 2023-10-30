using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : UnitManager
{
    public System.Func<int, List<Node>> OnPlantGetPath = null;

    private IProduct GetProjectile(Data.UnitData data)
    {
        return OnUnitGetProduct?.Invoke(data);
    }

    public override void AddUnit(IUnit unit)
    {
        base.AddUnit(unit);

        Plant plant = PUnitCast(unit);
        plant.OnGetPath = PlantGetPath;
        plant.OnGetProduct = GetProjectile;

        plant.InitializeRow();
    }

    private Plant PUnitCast(IUnit unit)
    {
        Plant pUnit = unit as Plant;
        return pUnit;
    }

    private List<Node> PlantGetPath(int row)
    {
        return OnPlantGetPath?.Invoke(row);
    }

}
