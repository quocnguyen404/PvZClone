using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ZombieObjectPool : ObjectPoolBase
{
    public Transform zombieTransform = null;

    private Zombie zombiePrefab = null;
    private readonly List<Zombie> zombies = new List<Zombie>();

    public System.Action<IUnit> OnSpawnUnit = null;

    public void InitializePool(LevelConfig levelConfig)
    {
        foreach (KeyValuePair<int, string> zombie in levelConfig.zombies)
        {
            zombiePrefab = Resources.Load<Zombie>(string.Format(GameConstant.ZOMBIE_PREFAB_PATH, zombie.Value));
            GenerateZombie(ConfigHelper.GameConfig.zombies[zombie.Value], zombie.Key);
        }
    }

    private void GenerateZombie(Data.UnitData unitData, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Zombie newZombie = Instantiate(zombiePrefab, transform);
            newZombie.UnitData = unitData;
            newZombie.gameObject.SetActive(true);

            OnSpawnUnit?.Invoke(newZombie);

            zombies.Add(newZombie);
        }
    }

    public Zombie GetZombie(Data.UnitData unitData)
    {
        Zombie zombie = zombies.Find(p => !p.gameObject.activeInHierarchy && p.UnitData.unitName == unitData.unitName);

        if (zombie != null)
        {
            zombie.gameObject.SetActive(true);
            return zombie;
        }

        if (zombies.Count < MAX_POOL_SIZE)
        {
            zombiePrefab = Resources.Load<Zombie>(string.Format(GameConstant.ZOMBIE_PREFAB_PATH, unitData.unitName));
            GenerateZombie(unitData, 1);

            Zombie lastZombie = zombies[^1];
            lastZombie.gameObject.SetActive(true);

            return lastZombie;
        }

        return null;
    }
}
