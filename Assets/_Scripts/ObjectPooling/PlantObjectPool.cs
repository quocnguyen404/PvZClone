using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantObjectPool : ObjectPoolBase
{
    public Plant plantPrefab = null;

    private readonly List<Plant> plants = new ();

    public void InitilizePool()
    {

    }
}
