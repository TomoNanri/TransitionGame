using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageController : MonoBehaviour
{
    public List<LevelDataAsset> LevelData = new List<LevelDataAsset>();

    [SerializeField] private GameObject _target;

    [SerializeField] private Vector3 _targetPos = new Vector3(0f, 1.8f, 18f);
    [SerializeField] private List<GameObject> _obstacles = new List<GameObject>();

    private GameManager _gameManager;
    [SerializeField] private int _currentLevel;

    private bool _isStageClean;
    private bool _isStageComplete;

    private TextMeshProUGUI _levelText;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;

        // Game Levelの初期化
        _currentLevel = _gameManager.StartLevel;
        _isStageClean = true;
        _levelText = GameObject.Find("Canvas/LevelText").GetComponent<TextMeshProUGUI>();
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
                    Vector3 pos = o.Position;
                    var obs = Instantiate(o.ObstaclePrefabs, pos, Quaternion.identity);
                    _obstacles.Add(obs);
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
                foreach(GameObject o in _obstacles)
                {
                    Destroy(o);
                }
                _obstacles.Clear();
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
