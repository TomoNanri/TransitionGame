using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum GameStateType { Intro, Instruction, InGame, GameOver}
    public GameStateType GameState => _gameState;
    public int StartLevel => _startLevel;
    public bool IsEndingStarted => _isEndingStarted;

    [SerializeField]
    private int _startLevel = 0;
    protected override bool dontDestroyOnLoad { get { return true; } }

    private GameStateType _gameState = GameStateType.Intro;
    private IntroPanel _intro;
    private bool _isEndOfOpening = false;
    private bool _isEndingStarted = false;
    private GameObject _endingPanel = null;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            _intro = GameObject.FindAnyObjectByType<IntroPanel>();
            _isEndOfOpening = false;
            _intro.EndOfOpening += EndOfOpeningEventHandler;
        }
        else if(SceneManager.GetActiveScene().name == "InGame")
        {
            _gameState = GameStateType.InGame;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (_gameState)
        {
            case GameStateType.Intro:
                if (_isEndOfOpening)
                {
                    if (Input.anyKeyDown)
                    {
                        _gameState = GameStateType.Instruction;
                        SceneManager.LoadScene("Instruction");
                    }
                }
                break;

            case GameStateType.Instruction:
                if (Input.anyKeyDown)
                {
                    _gameState = GameStateType.InGame;
                    SceneManager.LoadScene("InGame");
                }
                break;

            case GameStateType.InGame:
                if(_endingPanel == null)
                {
                    _endingPanel = GameObject.Find("Canvas/EndingPanel");
                    _endingPanel.SetActive(false);
                }
                break;

            case GameStateType.GameOver:
                if (!_isEndingStarted)
                {
                    _isEndingStarted= true;
                    _endingPanel.SetActive(true);
                }
                break;
        }
    }
    private void EndOfOpeningEventHandler()
    {
        _isEndOfOpening = true;
    }

    public void OnGameOver()
    {
        _gameState = GameStateType.GameOver;
        _isEndingStarted = false;
    }
}
