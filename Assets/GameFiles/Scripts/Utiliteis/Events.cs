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
    public delegate void PlayerLivesChangedEvent(int Lives);
    public event PlayerLivesChangedEvent onPlayerLivesChange;

    public void LivesChanged(int lives)
    {
        if(onPlayerLivesChange != null)
        {
            onPlayerLivesChange(lives);
        }
    }
    public delegate void PlayerGoldChangedEvent(int current, int target);
    public event PlayerGoldChangedEvent onPlayerGoldChange;

    public void PlayerGoldChanged(int current, int target)
    {
        if(onPlayerGoldChange != null)
        {
            onPlayerGoldChange(current, target);
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

    //sensor events
    public delegate void SensorPing();
    public event SensorPing onSensorPing;

    public void DoSensorPing()
    {
        if(onSensorPing != null)
        {
            onSensorPing();
        }
    }

    
    public delegate void SensorStateChange(bool state);
    public event SensorStateChange onSensorStateChange;

    public void DoSensorStateChange(bool state)
    {
        if (onSensorStateChange != null)
        {
            onSensorStateChange(state);
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
}
