using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Events _event;
    public Events.EventFadeComplete onFadecomplete;
    public Events.EventVolumChange onVolumChange;
    public Events.EventToggleSound onSoundToggle;
    private GameManager.GameState _currentState;

    //buttons
    [Header ("Main Screen UI")]
    [SerializeField] private Button _settings;
    [SerializeField] private Button _quit;
    [SerializeField] private Button _start;
    [SerializeField] private Button _info;

    [Space(2)]

    [Header("Settings screen buttons")]
    [SerializeField] private Button _backButton;

    [Space(2)]
    [Header("In Game Menu Buttons")]
    [SerializeField] private Button _inGameQuitButton;
    [SerializeField] private Button _inGameSettingButton;
    [SerializeField] private Button _inGameTitleButton;


    [Space(2)]
    [Header("Hud Variables")]
    [SerializeField] private TextMeshProUGUI _jumpText;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("Game Over Variables")]
    [SerializeField] private TextMeshProUGUI _gameOverGoldText;
    [SerializeField] private Button _gameOverQuit;
    [SerializeField] private Button _gameOverTitle;

    //UIgroups control
    [Header("UI Group controls")]
    [SerializeField] private GameObject _titleObject;
    [SerializeField] private CanvasGroup _titleGroup;
    [SerializeField] private GameObject _hudObject;
    [SerializeField] private CanvasGroup _hudGroup;
    [SerializeField] private GameObject _gameOverObject;
    [SerializeField] private CanvasGroup _gameOverGroup;
    [SerializeField] private GameObject _inGameMenuObject;
    [SerializeField] private CanvasGroup _inGameMenuGroup;
    [SerializeField] private GameObject _settingsObject;
    [SerializeField] private CanvasGroup _settingsGroup;
    [SerializeField] private GameObject _instrutionsObject;
    [SerializeField] private CanvasGroup _instructionsGroup;


    [Header("UIAmiators groups")]
    [SerializeField] private  Animator _hudAnimator;
    [SerializeField] private  Animator _mainMenuAnimator;
    [SerializeField] private  Animator _gameOverAnimator;
    [SerializeField] private Animator _settingsAnimator;
    [SerializeField] private Animator _inGameMenuAnimator;
    

    [Header("Level Text Group")]
    [SerializeField] private GameObject _levelTextObject;
    [SerializeField] private TextMeshProUGUI _levelText;
    private int _currentLevel = 0;

    [Header("Infoscreen")]
    [SerializeField] private Button _infoBackButton;



    private void ShowTitleUI()                   // Activate the objects for showing Title ui, later may have coroutine to add animation
    {

        _titleObject.SetActive(true);
        _hudObject.SetActive(false);
        _gameOverObject.SetActive(false);
        _settingsObject.SetActive(false);
        _instrutionsObject.SetActive(false);
        _inGameMenuObject.SetActive(false);
        _levelTextObject.SetActive(false);
        _start.onClick.AddListener(StartPressed);
        _quit.onClick.AddListener(QuitButtonPressed);
        _settings.onClick.AddListener(SettingsButtonPressed);
        _inGameSettingButton.onClick.AddListener(SettingsButtonPressed);
        _inGameQuitButton.onClick.AddListener(QuitButtonPressed);
        _inGameTitleButton.onClick.AddListener(InGameTitleButtonPressed);
        _backButton.onClick.AddListener(BackButtonPressed);
        _gameOverQuit.onClick.AddListener(QuitButtonPressed);
        _gameOverTitle.onClick.AddListener(InGameTitleButtonPressed);
        _info.onClick.AddListener(TitleInfoButtonPressed);
        _infoBackButton.onClick.AddListener(InfoBackButtonPressed);
        _event.onPlayerActive += PlayerActive;
        _event.PlayBGMSoundClip(0);

    }

    private void InfoBackButtonPressed()
    {
        _instrutionsObject.SetActive(false);
        _titleObject.SetActive(true);
    }

    private void TitleInfoButtonPressed()
    {
        _instrutionsObject.SetActive(true);
        _titleObject.SetActive(false);
    }
        

    private void SettingsButtonPressed()
    {
        // needs action for showing settings menu on title screen
        _settingsObject.SetActive(true);

    }

    private void BackButtonPressed()
    {
        _settingsObject.SetActive(false);
    }


    private void QuitButtonPressed()  // quit game button pressed
    {
        GameManager.Instance.QuitGame();
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);       //Make object carry over through scene loading 
    }

    public void StartPressed()
    {
        _event.StartButtonPressed();
    }

    public void InGameTitleButtonPressed()
    {
        Debug.Log("ingame title button pressed");
        GameManager.Instance.RestartGame();
    }


    private void OnEnable()   // register for game events when enabled
    {
        if(_event == null)  // add ref for events manager if not present
        {
            _event = Events.Instance;

        }

        _event.onPlayerHealthChange += ChangeHealthText;
        _event.onPlayerJumpChange += ChangeJumpText;
        GameManager.Instance.OnGameStateChanged.AddListener(GameStateChanged);
       

    }

    private void OnDisable()
    {
       _event.onPlayerHealthChange -= ChangeHealthText;
        _event.onPlayerJumpChange -= ChangeJumpText;

    }

    private void ChangeHealthText(int health)  // changes HUD text for number of lives when playing level, may later include icons
    {
        Debug.Log("Health Called");
        _healthText.text = "Health: " + health + "%";
    }

    private void ChangeJumpText(int jump)  // changes the gold value if collected
    {
        _jumpText.text = "Jump charge:  " + jump + " % ";
    }




    private void GameStateChanged(GameManager.GameState current, GameManager.GameState old) // handle game state changes
    {
        _currentState = current;
        switch (current)
        {
            case GameManager.GameState.PREGAME:
                break;
            case GameManager.GameState.RUNNING:
                if (old == GameManager.GameState.MAINMENU)
                {
                    LevelStart();
                }else if(old == GameManager.GameState.PAUSED)
                {
                    UnPause();
                }
                break;
            case GameManager.GameState.PAUSED:
                Paused();
                break;
            case GameManager.GameState.MAINMENU:
                if(old == GameManager.GameState.PREGAME)
                {

                    ShowTitleUI();
                }
                break;
            case GameManager.GameState.GAMEOVER:
                GameOver();
                break;
            default:
                break;
        }

    }

    public void FadeOutComplete(GameResources.uiGroupName name) // actions when UI fade animations end
    {
        switch (name)
        {
            case GameResources.uiGroupName.HUD:

                break;
            case GameResources.uiGroupName.TITLE:
                if (_currentState == GameManager.GameState.RUNNING)
                {
                    _titleObject.SetActive(false);
                    _hudObject.SetActive(true);
                    _hudAnimator.SetTrigger("FadeIn");
                    onFadecomplete.Invoke(false);
                }
                break;
            case GameResources.uiGroupName.GAMEOVER:
                break;
            case GameResources.uiGroupName.SETTINGS:
                if (_currentState == GameManager.GameState.MAINMENU)
                {
                    _titleObject.SetActive(true);
                    _settingsObject.SetActive(false);
                }
                else if (_currentState == GameManager.GameState.PAUSED)
                {
                    _inGameMenuObject.SetActive(true);
                    _settingsObject.SetActive(false);
                }
                break;
            default:
                break;

        }


    }
    public void FadeInComplete(GameResources.uiGroupName name)
    {
        switch (name)
        {
            case GameResources.uiGroupName.HUD:
                StartCoroutine(ShowLevelText(true));
                break;
            case GameResources.uiGroupName.TITLE:

                break;
            case GameResources.uiGroupName.GAMEOVER:
                break;
            case GameResources.uiGroupName.SETTINGS:
                if (_currentState == GameManager.GameState.MAINMENU)
                {
                    _titleObject.SetActive(false);
                }
                else if (_currentState == GameManager.GameState.PAUSED)
                {
                    _inGameMenuObject.SetActive(false);
                }
                break;
            default:
                break;

        }

    }
    
    private void LevelStart() // actions for the start of a the game
    {
        
        _mainMenuAnimator.SetTrigger("FadeOut");
    }

    private void GameOver() // called when all lives lost
    {

        _titleObject.SetActive(false);
        _hudObject.SetActive(false);
        _gameOverObject.SetActive(true);
        _settingsObject.SetActive(false);
        _instrutionsObject.SetActive(false);
        _inGameMenuObject.SetActive(false);
        GameManager.Instance.UnloadLevel("Main");


        
    }

    private void Paused()
    {
        _inGameMenuObject.SetActive(true);
    }

    private void UnPause()
    {
        _inGameMenuObject.SetActive(false);
    }


    public void VolumeChanger(GameResources.volumeType type, float val)
    {
        onVolumChange.Invoke(type, val);
    }

    public void AudioToggled(GameResources.volumeType type, bool state)
    {
        onSoundToggle.Invoke(type, state);
    }

    private void StartNextLevel()
    {
        _event.StartLevel();
    }

    private void ChangingLevel(int LevelNo)
    {
        _currentLevel = LevelNo;
        StartCoroutine(ShowLevelText(false));
    }

    IEnumerator ShowLevelText(bool firstLevel)
    {
        
        _levelText.text = "LEVEL" + (_currentLevel + 1);
        _levelTextObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _levelText.text = "START";
        yield return new WaitForSeconds(2);
        _levelTextObject.SetActive(false);
        if (firstLevel)
        {
            _event.StartTheLevel();
            
        }
        else
        {

            StartNextLevel();
        }

    }

    public void PlayerActive()
    {
     //   Player.Instance.levelChanged.AddListener(ChangingLevel);
    }
    
}
