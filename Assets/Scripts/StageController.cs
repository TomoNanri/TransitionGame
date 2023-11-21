using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageController : MonoBehaviour
{
    public List<LevelDataAsset> LevelData = new List<LevelDataAsset>();

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _obstacle1;
    [SerializeField] private GameObject _obstacle2;

    [SerializeField] private Vector3 _targetPos = new Vector3(0f, 1.8f, 18f);
    [SerializeField] private Vector3 _obstacle1Pos = new Vector3(0f, 4.5f, 15f);
    [SerializeField] private Vector3 _obstacle2Pos = new Vector3(0f, 6f, 12f);

    private GameManager _gameManager;
    [SerializeField] private int _currentLevel;

    private bool _isStageCleared;
    private bool _isStageComplete;

    private TextMeshProUGUI _levelText;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;

        // Game LevelÇÃèâä˙âª
        _currentLevel = _gameManager.StartLevel;
        _isStageCleared = true;
        _levelText = GameObject.Find("Canvas/LevelText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // InGameÇ…Ç»Ç¡ÇΩÇÁìIÇê∂ê¨Ç∑ÇÈ
        if (_gameManager.GameState == GameManager.GameStateType.InGame)
        {
            if (_isStageCleared == true)
            {
                _isStageComplete = false;
                _levelText.SetText($"ì¡åP {_currentLevel+1} ì˙ñ⁄");
                _target = Instantiate(LevelData[_currentLevel].TargetPrefab, _targetPos, Quaternion.identity);
                var coin = _target.transform.Find("Coin").GetComponent<Coin>();
                coin.BrokenAction += TargetBrokenActionHandler;
                if (LevelData[_currentLevel].Obstacle1 != null)
                {
                    _obstacle1Pos = LevelData[_currentLevel].Obs1Position;
                    _obstacle1 = Instantiate(LevelData[_currentLevel].Obstacle1, _obstacle1Pos, Quaternion.identity);
                }
                if (LevelData[_currentLevel].Obstacle2 != null)
                {
                    _obstacle2Pos = LevelData[_currentLevel].Obs2Position;
                    _obstacle2 = Instantiate(LevelData[_currentLevel].Obstacle2, _obstacle2Pos, Quaternion.identity);
                }
                _isStageCleared = false;
            }
            if(_isStageComplete == true)
            {
                _isStageComplete = false;
                if(_target != null)
                {
                    Destroy( _target );
                }
                if(_obstacle1 != null)
                {
                    Destroy( _obstacle1 );
                }
                if(_obstacle2 != null)
                {
                    Destroy( _obstacle2 );
                }
                _isStageCleared = true ;
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
