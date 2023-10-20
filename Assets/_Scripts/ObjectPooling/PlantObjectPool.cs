using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantObjectPool : ObjectPoolBase
{
    private Plant unitPrefab = null;

    private int initPlantAmount = 10;
    private readonly List<Plant> plants = new();

    public void InitilizePool(List<Data.UnitData> plantData)
    {
        foreach (Data.UnitData data in plantData)
        {
            for (int i = 0; i < initPlantAmount; i++)
                GeneratePlant(data);
        }
    }

    private void GeneratePlant(Data.UnitData unitdata)
    {
        unitPrefab = Resources.Load<Plant>(string.Format(GameConstant.PLANT_PREFAB_PATH, unitdata.unitName));
        Plant newPlant = Instantiate(unitPrefab, transform);
        newPlant.unitData = unitdata;
        newPlant.gameObject.SetActive(false);
        plants.Add(newPlant);
    }


    public Plant GetPlant(Data.UnitData unitData)
    {
        foreach (Plant plant in plants)
        {
            if (!plant.gameObject.activeInHierarchy && plant.unitData.unitName == unitData.unitName)
            {
                plant.gameObject.SetActive(true);
                return plant;
            }
        }

        if (plants.Count < MAX_POOL_SIZE)
        {
            GeneratePlant(unitData);

            Plant lastPlant = plants[^1];
            lastPlant.gameObject.SetActive(true);

            return lastPlant;
        }

        return null;

    }
}
