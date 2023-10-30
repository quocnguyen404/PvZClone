using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductObjectPool : ObjectPoolBase
{
    private IProduct productPrefab = null;

    private int initProjectileAmount = 10;

    private readonly List<IProduct> products = new List<IProduct>();

    public void InitializePool(List<Data.UnitData> plantData)
    {
        foreach (Data.UnitData data in plantData)
        {
            if (data.productName != "")
            {
                productPrefab = Resources.Load<IProduct>(string.Format(GameConstant.PROJECTILE_PREFAB_PATH, data.productName));

                for (int i = 0; i < initProjectileAmount; i++)
                    GenerateProjectile(data);
            }
        }
    }

    private void GenerateProjectile(Data.UnitData plantData)
    {
        IProduct newProjectile = Instantiate(productPrefab, transform);
        newProjectile.gameObject.SetActive(false);
        products.Add(newProjectile);
    }

    public IProduct GetProjectile(Data.UnitData plantData)
    {
        IProduct projectile = products.Find(p => !p.gameObject.activeInHierarchy && plantData.productName == p.projectileName);

        if (projectile != null)
        {
            projectile.gameObject.SetActive(true);
            return projectile;
        }

        if (products.Count < MAX_POOL_SIZE)
        {
            productPrefab = Resources.Load<IProduct>(string.Format(GameConstant.PROJECTILE_PREFAB_PATH, plantData.productName));
            GenerateProjectile(plantData);

            IProduct lastProjectile = products[^1];
            lastProjectile.gameObject.SetActive(true);

            return lastProjectile;
        }

        return null;
    }
}
