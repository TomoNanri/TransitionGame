using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBFactory : Factory
{
    private const string _prefabPath = "PhysicsObstacleB";
    protected override Product CreateProduct(Transform parent, Vector3 position, string description)
    {
        GameObject prefab = (GameObject)Resources.Load(_prefabPath, typeof(GameObject));
        if (prefab == null)
        {
            Debug.LogError($"[FactoryB] prefab = {prefab}");
            return null;
        }
        GameObject obs = GameObject.Instantiate(prefab, position, Quaternion.identity);
        if (obs == null)
        {
            Debug.LogError($"[FactoryA] obs = {obs}");
            return null;
        }
        obs.transform.parent = parent;
        obs.AddComponent(typeof(ObstacleB));
        ObstacleB obstacleB = obs.GetComponent<ObstacleB>();
        obstacleB.Description = description;
        return obstacleB;
    }
    protected override void DeleteProduct(Product p)
    {
        GameObject.Destroy(p.gameObject);
    }
}
