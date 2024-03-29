using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductObjectPool : ObjectPoolBase
{
    private IProduct productPrefab = null;

    private int initProjectileAmount = 10;

    private readonly List<IProduct> products = new List<IProduct>();

    public System.Action<IProduct> OnSpawnProduct = null;

    public void InitializePool(List<Data.UnitData> plantData)
    {
        foreach (Data.UnitData data in plantData)
        {
            if (data.productName != "")
            {
                productPrefab = Resources.Load<IProduct>(string.Format(GameConstant.PROJECTILE_PREFAB_PATH, data.productName));

                for (int i = 0; i < initProjectileAmount; i++)
                    GenerateProduct(data);
            }
        }
    }

    private void GenerateProduct(Data.UnitData plantData)
    {
        IProduct newProjectile = Instantiate(productPrefab, transform);
        newProjectile.gameObject.SetActive(false);
        products.Add(newProjectile);

        OnSpawnProduct?.Invoke(newProjectile);
    }

    public IProduct GetProduct(Data.UnitData plantData)
    {
        IProduct projectile = products.Find(p => !p.gameObject.activeInHierarchy && plantData.productName == p.productName);

        if (projectile != null)
        {
            projectile.gameObject.SetActive(true);
            projectile.gameObject.transform.SetAsLastSibling();
            return projectile;
        }

        if (products.Count < MAX_POOL_SIZE)
        {
            productPrefab = Resources.Load<IProduct>(string.Format(GameConstant.PROJECTILE_PREFAB_PATH, plantData.productName));
            GenerateProduct(plantData);

            IProduct lastProjectile = products[^1];
            lastProjectile.gameObject.SetActive(true);

            return lastProjectile;
        }

        return null;
    }
}
