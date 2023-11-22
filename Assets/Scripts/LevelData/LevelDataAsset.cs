using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "LevelData/Create Level Data Asset")]
public class LevelDataAsset : ScriptableObject
{
    public int StageLevel;
    public GameObject TargetPrefab;
    public List<Obstacle> Obstacles = new List<Obstacle>();
}
[Serializable]
public struct Obstacle
{
    public GameObject ObstaclePrefabs;
    public Vector3 Position;
}
