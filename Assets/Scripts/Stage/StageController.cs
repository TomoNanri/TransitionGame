using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class StageController : MonoBehaviour
{
    public List<LevelDataAsset> LevelData = new List<LevelDataAsset>();

    [SerializeField] private GameObject _target;

    [SerializeField] private Vector3 _targetPos = new Vector3(0f, 1.8f, 18f);

    private GameManager _gameManager;
    [SerializeField] private int _currentLevel;

    private bool _isStageClean;
    private bool _isStageComplete;

    private TextMeshProUGUI _levelText;
    private ObstacleAFactory _factoryA;
    private ObstacleBFactory _factoryB;
    private List<Product> _products = new List<Product>();

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;

        // Game Levelの初期化
        _currentLevel = _gameManager.StartLevel;
        _isStageClean = true;
        _levelText = GameObject.Find("Canvas/LevelText").GetComponent<TextMeshProUGUI>();

        // 障害物のファクトリー生成
        _factoryA = new ObstacleAFactory();
        _factoryB = new ObstacleBFactory();
    }

    // Update is called once per frame
    void Update()
    {
        // InGameになったら的を生成する
        if (_gameManager.GameState == GameManager.GameStateType.InGame)
        {
            if (_isStageClean == true)
            {
                _isStageComplete = false;
                _levelText.SetText($"特訓 {_currentLevel+1} 日目");
                _target = Instantiate(LevelData[_currentLevel].TargetPrefab, _targetPos, Quaternion.identity);
                var coin = _target.transform.Find("Coin").GetComponent<Coin>();
                coin.BrokenAction += TargetBrokenActionHandler;

                foreach(Obstacle o in LevelData[_currentLevel].Obstacles)
                {
                    switch (o.Type)
                    {
                        case Obstacle.ObstacleType.ObstacleA:
                            _products.Add(_factoryA.Create(transform, o.Position, $"{_currentLevel}"));
                            break;

                        case Obstacle.ObstacleType.ObstacleB:
                            _products.Add(_factoryB.Create(transform, o.Position, $"{_currentLevel}"));
                            break;
                        default:
                            Debug.LogError($"[{name}] Illegal Obstacle Type!");
                            break;
                    }
                }
                _isStageClean = false;
            }
            if(_isStageComplete == true)
            {
                _isStageComplete = false;
                if(_target != null)
                {
                    Destroy( _target );
                }
                foreach (Product p in _products)
                {
                    if (p is ObstacleA)
                    {
                        _factoryA.Delete(p);
                    }
                    else if(p is ObstacleB)
                    {
                        _factoryB.Delete(p);
                    }
                    else
                    {
                        Debug.LogError($"[{name}] Illegal Obstacle Type!");
                    }
                }
                _products.Clear();
                _isStageClean = true ;
            }
        }
    }
    private void TargetBrokenActionHandler()
    {
        _isStageComplete = true;
        _currentLevel++;
        if(_currentLevel >= LevelData.Count)
        {
            _gameManager.OnGameOver();
        }
    }
}
