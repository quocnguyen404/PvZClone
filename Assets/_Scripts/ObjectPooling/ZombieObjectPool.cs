using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ZombieObjectPool : ObjectPoolBase
{
    public Transform zombieTransform = null;

    private Zombie zombiePrefab = null;
    private readonly List<Zombie> zombies = new List<Zombie>();
    private Dictionary<string, int> minZombie;

    public System.Action<IUnit> OnSpawnUnit = null;

    public void InitializePool()
    {
        CalculateMinimumZombie();

        foreach (var zombie in minZombie)
        {
            zombiePrefab = Resources.Load<Zombie>(string.Format(GameConstant.ZOMBIE_PREFAB_PATH, zombie.Key));
            GenerateZombie(ConfigHelper.GameConfig.zombies[zombie.Key], zombie.Value);
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

    private void CalculateMinimumZombie()
    {
        minZombie = new Dictionary<string, int>();

        foreach (PhaseData phase in ConfigHelper.GetCurrentLevelConfig().phases)
        {
            foreach (Batch zombie in phase.batchs)
            {
                if (minZombie.ContainsKey(zombie.name))
                {
                    if (zombie.amount > minZombie[zombie.name] - 1)
                        minZombie[zombie.name] = zombie.amount + 1;
                }
                else
                    minZombie.Add(zombie.name, zombie.amount);
            }
        }
    }

    public Zombie GetZombie(Data.UnitData unitData)
    {
        Zombie zombie = zombies.Find(p => !p.gameObject.activeInHierarchy && p.Name == unitData.unitName);

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
