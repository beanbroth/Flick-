using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameState _state;
    private bool _shouldUpdateState;

    private GameState _nextSate;

    [SerializeField] bool _startLevelOverride;
    [SerializeField] int overRideLevel;

    [SerializeField] private String[] scenes;
    [SerializeField] int nextLevel;

    public static event Action<GameState> OnGameStateChanged;

    public static GameManager Instance { get { return _instance; } }

    public GameState CurrentState { get => _state; }

    public enum GameState
    {
        StartScreen,
        PlayingLevel,
        PauseMenu,
        LossScreen
    }

    private void Start()
    {
        nextLevel = 0;
        MakeSingleton();
        _shouldUpdateState = true;

        if (_startLevelOverride)
        {
            nextLevel = overRideLevel;
        }
    }

    private void Update()
    {
        if (!_shouldUpdateState)
            return;

        _shouldUpdateState = false;
        _state = _nextSate;
        UpdateGameState(_nextSate);
    }

    public void SetNextGameState(GameState newState)
    {
        _nextSate = newState;
        _shouldUpdateState = true;
    }

    private void UpdateGameState(GameState newState)
    {
        _state = newState;

        switch (_state)
        {
            case GameState.StartScreen:
                break;
            case GameState.PlayingLevel:
                break;
            case GameState.PauseMenu:
                break;
            case GameState.LossScreen:
                break;
            default:
                break;
        }
        Debug.Log(_state);
        OnGameStateChanged?.Invoke(_state);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(scenes[nextLevel]);
        nextLevel++;
    }

    private void MakeSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
}
