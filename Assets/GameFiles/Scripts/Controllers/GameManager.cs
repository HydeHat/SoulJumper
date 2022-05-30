using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    //keep track of the game state

    public enum GameState
    {
        PREGAME,
        MAINMENU,
        GAMEOVER,
        RUNNING,
        PAUSED,
        RESTART
    }
    


    public GameObject[] SystemPrefabs;
    public Events.EventGameState OnGameStateChanged;
    

    private List<GameObject> _instanceSystemPrefabs;

    private GameState _previousGameState;

    private string _currentLevelName = string.Empty;
    private string _prevLevelName = string.Empty;

    private Player _player;

    List<AsyncOperation> _loadOperations;

    GameState _currentGameState = GameState.PREGAME;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);


        _instanceSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();

        Events.Instance.onUIStartButtonPressed += StartGame;
        UIManager.Instance.onFadecomplete.AddListener(HandleFadeMenuComplete);
        _player = GameResources._player;


        
    }

    private void Update()
    {

        if (_currentGameState == GameManager.GameState.PREGAME)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Escape) && _currentGameState == GameState.RUNNING || Input.GetKeyDown(KeyCode.Escape) && _currentGameState == GameState.PAUSED)
        {
            TogglePause();
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {

        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if (_loadOperations.Count == 0)
            {
                
                if(_currentLevelName == "Level 1")
                {
                    UpdateState(GameState.RUNNING);

                }
                if(_currentLevelName == "GameOver")
                {
                    UpdateState(GameState.GAMEOVER);
                }
                if (_currentGameState == GameState.RESTART)
                {
                    if (_previousGameState == GameState.PAUSED)
                    {
                        UnloadLevel(_currentLevelName);
                        GameObject.Find("RestartManager").GetComponent<RestartManger>().Restart(_instanceSystemPrefabs.ToArray());
                    }
                    else if (_previousGameState == GameState.GAMEOVER)
                    {
                        UnloadLevel("GameOver");
                        GameObject.Find("RestartManager").GetComponent<RestartManger>().Restart(_instanceSystemPrefabs.ToArray());
                    }
                }
                if(_currentGameState == GameState.RUNNING)
                {

                }

            }
        }

        Debug.Log("Load Complete" );
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Operation Complete");
    }

    void UpdateState(GameState state)
    {
         _previousGameState = _currentGameState;
        _currentGameState = state;
        

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            case GameState.MAINMENU:
                Time.timeScale = 1.0f;
                break;
            case GameState.GAMEOVER:
                Time.timeScale = 1.0f;
                break;
            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, _previousGameState);

    }

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    
    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instanceSystemPrefabs.Add(prefabInstance);
        }
        _currentLevelName = "Title";
        
        UpdateState(GameState.MAINMENU);
    }

    public void LoadLevel(string levelName)
    {

        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        if (_loadOperations == null)
        {
            _loadOperations = new List<AsyncOperation>();
        }
        _loadOperations.Add(ao);
        _prevLevelName = _currentLevelName;
        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        Debug.Log("Unload level name: " + levelName);
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] unable to unload level " + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for(int i = 0; i < _instanceSystemPrefabs.Count; i++)
        {
            Destroy(_instanceSystemPrefabs[i]);
        }
        _instanceSystemPrefabs.Clear();
    }


    public void StartGame()
    {

        LoadLevel("Level 1");
        GameResources.currentLevel = 1;
    }

    public void TogglePause()
    {
        UpdateState( _currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {

        LoadLevel("Restart");
        UpdateState(GameState.RESTART);
    }

    public void QuitGame()
    {

        Application.Quit();
    }
   void HandleFadeMenuComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(_prevLevelName);
        }
    }
}
