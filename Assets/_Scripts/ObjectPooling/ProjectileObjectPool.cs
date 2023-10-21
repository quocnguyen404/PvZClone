using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : ObjectPoolBase
{
    private Projectile projectilePrefab = null;

    private int initProjectileAmount = 10;

    private readonly List<Projectile> projectiles = new List<Projectile>();

    public void InitializePool(List<Data.UnitData> plantData)
    {
        foreach (Data.UnitData data in plantData)
        {
            if (data.productName != null)
            {
                projectilePrefab = Resources.Load<Projectile>(string.Format(GameConstant.PROJECTILE_PREFAB_PATH, data.productName));

                for (int i = 0; i < initProjectileAmount; i++)
                    GenerateProjectile(data);
            }
        }
    }

    private void GenerateProjectile(Data.UnitData plantData)
    {
        Projectile newProjectile = Instantiate(projectilePrefab, transform);
        newProjectile.InitProjectile(plantData);
        newProjectile.gameObject.SetActive(false);
        projectiles.Add(newProjectile);
    }

    public Projectile GetProjectile(Data.UnitData plantData)
    {
        Projectile projectile = projectiles.Find(p => !p.gameObject.activeInHierarchy && plantData.productName == p.projectileName);

        if (projectile != null)
        {
            projectile.gameObject.SetActive(true);
            return projectile;
        }

        if (projectiles.Count < MAX_POOL_SIZE)
        {
            projectilePrefab = Resources.Load<Projectile>(string.Format(GameConstant.PROJECTILE_PREFAB_PATH, plantData.productName));
            GenerateProjectile(plantData);

            Projectile lastProjectile = projectiles[^1];
            lastProjectile.gameObject.SetActive(true);

            return lastProjectile;
        }

        return null;
    }
}
