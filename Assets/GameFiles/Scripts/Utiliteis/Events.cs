using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : Singleton<Events>
{
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    [System.Serializable] public class EventVolumChange : UnityEvent<GameResources.volumeType, float> { }
    [System.Serializable] public class EventToggleSound : UnityEvent<GameResources.volumeType, bool> { }
    [System.Serializable] public class EventLevelChanged : UnityEvent<int> { }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // UI hud events 
    public delegate void PlayerHealthChangedEvent(int health);
    public event PlayerHealthChangedEvent onPlayerHealthChange;

    public void HealthChanged(int health)
    {
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(health);
        }
    }
    public delegate void PlayerJumpChangedEvent(int jump);
    public event PlayerJumpChangedEvent onPlayerJumpChange;

    public void PlayerJumpChanged(int jump)
    {
        if(onPlayerJumpChange != null)
        {
            onPlayerJumpChange(jump);
        }
    }

    // ui button press events
    public delegate void UIStartButtonPressed();
    public event UIStartButtonPressed onUIStartButtonPressed;

    public void StartButtonPressed()
    {
        if(onUIStartButtonPressed != null)
        {
            onUIStartButtonPressed();
        }
    }

    public delegate void LevelReadyToStart();
    public event LevelReadyToStart onLevelReadyToStart;

    public void StartTheLevel()
    {
        if(onLevelReadyToStart != null)
        {
            onLevelReadyToStart();
        }
    }





    // play UI Sounds 

    public delegate void PlayUISound(int clipNo);
    public event PlayUISound onPlayUISound;

    public void PlayUISoundClip(int clipNo)
    {
        onPlayUISound(clipNo);
    }
    //play FX sounds
    public delegate void PlayFXSound(int clipNo);
    public event PlayUISound onPlayFXSound;

    public void PlayFXSoundClip(int clipNo)
    {
        onPlayFXSound(clipNo);
    }

    //Play BGM
    public delegate void PlayBGMSound(int clipNo);
    public event PlayBGMSound onPlayBGMSound;

    public void PlayBGMSoundClip(int clipNo)
    {
        onPlayBGMSound(clipNo);
    }

    public delegate void LevelStart();
    public event LevelStart onLevelStart;

    public void StartLevel()
    {
        onLevelStart();
    }

    public delegate void PlayerActive();
    public event PlayerActive onPlayerActive;

    public void SetPlayerActive()
    {
        onPlayerActive();
    }

    public delegate void ChangeLevel();
    public event ChangeLevel onChangeLevel;

    public void SetChangeLevel(int level)
    {
        GameManager.Instance.LoadLevel("Level " + level);
    }
}
