using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Factory
{
    public Product Create(Transform parent, Vector3 position, string description)
    {
        Product p = CreateProduct(parent, position, description);
        return p;
    }
    public void Delete(Product p)
    {
        DeleteProduct(p);
    }
    protected abstract Product CreateProduct(Transform parent, Vector3 position, string description);
    protected abstract void DeleteProduct(Product p);
}
