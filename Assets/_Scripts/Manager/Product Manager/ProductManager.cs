using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public List<IProduct> products = null;

    public virtual void Initialize()
    {
        products = new List<IProduct>();
    }

    public virtual void AddProduct(IProduct product)
    {
        products.Add(product);
    }

    public virtual void RemoveProduct(IProduct product)
    {
        products.Remove(product);
    }
}
