using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory
{
    protected List<GameObject> _products;
    public void Create(Transform parent, LevelDataAsset levelData)
    {
        CreateProduct(parent, levelData);
        return;
    }
    public void DestroyAll()
    {
        DestroyAllProduct();
    }
    protected abstract void CreateProduct(Transform parent, LevelDataAsset levelData);
    protected abstract void DestroyAllProduct();
}
