using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum GameStateType { Intro, Instruction, InGame}
    public GameStateType GameState => _gameState;
    public int StartLevel => _startLevel;

    [SerializeField]
    private int _startLevel = 0;
    protected override bool dontDestroyOnLoad { get { return true; } }

    private GameStateType _gameState = GameStateType.Intro;
    private IntroPanel _intro;
    private bool _isEndOfOpening = false;

    // Start is called before the first frame update
    void Start()
    {
        _intro = GameObject.FindAnyObjectByType<IntroPanel>();
        _isEndOfOpening = false;
        _intro.EndOfOpening += EndOfOpeningEventHandler;
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
                        Debug.Log($"[{name}] Start Loading Instruction Scene!");
                        _gameState = GameStateType.Instruction;
                        SceneManager.LoadScene("Instruction");
                    }
                }
                break;

            case GameStateType.Instruction:
                if (Input.anyKeyDown)
                {
                    Debug.Log($"[{name}] Start Loading InGame Scene!");
                    _gameState = GameStateType.InGame;
                    SceneManager.LoadScene("InGame");
                }
                break;

            case GameStateType.InGame:
                break;
        }

    }
    private void EndOfOpeningEventHandler()
    {
        _isEndOfOpening = true;
    }
}
