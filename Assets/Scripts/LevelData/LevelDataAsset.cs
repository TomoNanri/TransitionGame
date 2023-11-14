using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "LevelData/Create Level Data Asset")]

public class LevelDataAsset : ScriptableObject
{
    public int StageLevel => _stageLevel;
    public GameObject TargetPrefab => _targetPrefab;
    public GameObject Obstacle1 => _obstacle1;
    public Vector3 Obs1Position => _obs1Position;
    public GameObject Obstacle2 => _obstacle2;
    public Vector3 Obs2Position => _obs2Position;

    [SerializeField] private int _stageLevel;
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private GameObject _obstacle1;
    [SerializeField] private Vector3 _obs1Position;
    [SerializeField] private GameObject _obstacle2;
    [SerializeField] private Vector3 _obs2Position;
}
