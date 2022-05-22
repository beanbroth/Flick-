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


    [SerializeField] private GameObject _playerRef;

    [SerializeField] bool _startLevelOverride;

    [SerializeField] private String[] scenes;
    private int nextLevel;

    public static event Action<GameState> OnGameStateChanged;

    public static GameManager Instance { get { return _instance; } }

    public GameState CurrentState { get => _state; }
    public GameObject PlayerRef { get => _playerRef; }

    public enum GameState
    {
        StartScreen,
        LoadingLevel,
        PlayingLevel,
        PauseMenu,
        LossScreen
    }

    private void Start()
    {

        MakeSingleton();

        if (_startLevelOverride)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i] == SceneManager.GetSceneAt(1).name)
                {
                    nextLevel = i + 1;
                }
            }
        }
        else
        {
            LoadSpecificLevel(0);
        }
    }

    public void LoadSpecificLevel(int levelIndex)
    {
        nextLevel = levelIndex;
        SetNextGameState(GameState.LoadingLevel);
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
            case GameState.LoadingLevel:
                HandleLoadingLevel();

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

    private void HandleLoadingLevel()
    {
        Time.timeScale = 0f;
        StartCoroutine(DelayedStartPlayingLevel());
    }

    private IEnumerator DelayedStartPlayingLevel()
    {

        yield return new WaitForSecondsRealtime(1f);
        LoadNextScene();
        Time.timeScale = 1;
        UpdateGameState(GameState.PlayingLevel);

    }

    public void LoadNextScene()
    {
        UnloadAllScenesExcept("BaseScene");
        SceneManager.LoadScene(scenes[nextLevel], LoadSceneMode.Additive);
        nextLevel++;
    }

    //credit: https://answers.unity.com/questions/1305859/unload-all-scenes-except-one.html
    void UnloadAllScenesExcept(string sceneName)
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            print(scene.name);
            if (scene.name != sceneName)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
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
