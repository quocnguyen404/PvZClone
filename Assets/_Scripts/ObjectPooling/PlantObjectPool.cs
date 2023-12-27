using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantObjectPool : ObjectPoolBase
{
    private Plant plantPrefab = null;

    private int initPlantAmount = 10;
    private readonly List<Plant> plants = new List<Plant>();

    public void InitilizePool(List<Data.UnitData> plantData)
    {
        foreach (Data.UnitData data in plantData)
        {
            plantPrefab = Resources.Load<Plant>(string.Format(GameConstant.PLANT_PREFAB_PATH, data.unitName));

            for (int i = 0; i < initPlantAmount; i++)
                GeneratePlant(data);
        }
    }

    private void GeneratePlant(Data.UnitData unitdata)
    {
        Plant newPlant = Instantiate(plantPrefab, transform);
        newPlant.UnitData = unitdata;
        newPlant.gameObject.SetActive(false);
        plants.Add(newPlant);
    }


    public Plant GetPlant(Data.UnitData unitData)
    {
        Plant plant = plants.Find(p => !p.gameObject.activeInHierarchy && p.Name == unitData.unitName);

        if (plant != null)
        {
            plant.gameObject.SetActive(true);
            plant.gameObject.transform.SetAsLastSibling();
            return plant;
        }

        if (plants.Count < MAX_POOL_SIZE)
        {
            plantPrefab = Resources.Load<Plant>(string.Format(GameConstant.PLANT_PREFAB_PATH, unitData.unitName));
            GeneratePlant(unitData);

            Plant lastPlant = plants[^1];
            lastPlant.gameObject.SetActive(true);

            return lastPlant;
        }

        return null;
    }
}
