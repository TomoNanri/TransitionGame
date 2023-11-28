using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleAFactory : Factory
{
    private const string _prefabPath = "PhysicsObstacleA";
    protected override Product CreateProduct(Transform parent, Vector3 position, string description)
    {
        GameObject prefab = (GameObject)Resources.Load(_prefabPath);
        if (prefab == null)
        {
            Debug.LogError($"[FactoryA] prefab = {prefab}");
            return null;
        }
        GameObject obs = GameObject.Instantiate(prefab, position, Quaternion.identity);
        if (obs == null)
        {
            Debug.LogError($"[FactoryA] obs = {obs}");
            return null;
        }
        obs.transform.parent = parent;
        obs.AddComponent(typeof(ObstacleA));
        ObstacleA obstacleA = obs.GetComponent<ObstacleA>();
        obstacleA.Description = description;
        return obstacleA;
    }
    protected override void DeleteProduct(Product p)
    {
        GameObject.Destroy(p.gameObject);
    }
}
