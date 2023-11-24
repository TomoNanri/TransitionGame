using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : Factory
{
    public ObstacleFactory()
    {
        _products = new List<GameObject>();
    }
    protected override void CreateProduct(Transform parent, LevelDataAsset levelData)
    {
        foreach (Obstacle o in levelData.Obstacles)
        {
            Vector3 pos = o.Position;
            var obs = GameObject.Instantiate(o.ObstaclePrefabs, pos, Quaternion.identity);
            obs.transform.parent = parent;
            _products.Add(obs);
        }
    }
    protected override void DestroyAllProduct()
    {
        if (_products.Count == 0)
            return;
        foreach (GameObject p in _products)
        {
            GameObject.Destroy(p);
        }
        _products.Clear();
    }
}
