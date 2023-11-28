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
    public enum ObstacleType { ObstacleA=0, ObstacleB=1}
    public ObstacleType Type;
    public Vector3 Position;
}
