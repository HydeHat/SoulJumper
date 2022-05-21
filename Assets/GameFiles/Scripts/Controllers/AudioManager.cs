using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }



    // add volumn controls and link to ui
    GameObject audioManagerObj;
    [Header("Sound Volums")]
     [SerializeField] float _bmgVolume;
    [SerializeField] float _mainVolume;
    [SerializeField] float _sfxVolume;
    [SerializeField] float _uiVolume;
    [Space(2)]
    [Header("Audio source pools")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _uiSource;
    [SerializeField] AudioResources sources;
    [SerializeField] List<AudioSource> _playOnceSourcePoolFX;
    [SerializeField] int _playOncePoolSize;

    [Header("Audio Mixers")]
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] AudioMixerGroup mainMix;
    [SerializeField] AudioMixerGroup bMG;
    [SerializeField] AudioMixerGroup sFX;
    [SerializeField] AudioMixerGroup UI;

    [SerializeField] private int _bgmIngame;
    [SerializeField] private int _bgmTitle;
    [SerializeField] private int _bgmGameOver;
    [SerializeField] private int _currentBackgroundMusic;

    private float previousMasterVolum;

    private Events _events;
    

    public float bmgVolume
    {
        get { return _bmgVolume; }
    }

    public float mainVolume
    {
        get { return _mainVolume; }
    }

    public float sfxVolume
    {
        get { return _sfxVolume; }
    }
    public float UIVolume
    {
        get { return _uiVolume; }
    }

    

    private void OnEnable()
    {
        audioManagerObj = gameObject;
        if(_events == null)
        {
            _events = Events.Instance;
        }
        UIManager.Instance.onVolumChange.AddListener(ChangeVolume);
        UIManager.Instance.onSoundToggle.AddListener(SoundToggled);
        _events.onPlayUISound += PlayOnceUI;
        _events.onPlayFXSound += PlayOnceSFX;
        _events.onPlayBGMSound += PlayBGMLoop;
        
    }

    private void OnDisable()
    {
        _events.onPlayUISound -= PlayOnceUI;
        _events.onPlayFXSound -= PlayOnceSFX;
    }



    private void AddSourceToPlayOncePool()
    {
        AudioSource source = audioManagerObj.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = sFX;
        _playOnceSourcePoolFX.Add(source);
        
    }

    #region volume controls

    private void ChangeVolume(GameResources.volumeType type, float value)
    {
        switch (type)
        {
            case GameResources.volumeType.BGM:
                ChangeBGMVolume(value);
                break;
            case GameResources.volumeType.MASTER:
                ChangeMainVolume(value);
                break;
            case GameResources.volumeType.SFX:
                ChangeSFXVolume(value);
                break;
            case GameResources.volumeType.UI:
                ChangeUIVolume(value);
                break;
        }
    }




    private void ChangeBGMVolume(float newVolume)
    {
        _bmgVolume = newVolume;
        _audioMixer.SetFloat("BMG", _bmgVolume);
    }

    private void ChangeSFXVolume(float newVolume)
    {
        _sfxVolume = newVolume;
        _audioMixer.SetFloat("SFX", _sfxVolume);
    }

    private void ChangeUIVolume(float newVolume)
    {
        _sfxVolume = newVolume;
        _audioMixer.SetFloat("UI", _uiVolume);
    }

    private void ChangeMainVolume(float newVolume)
    {
        _mainVolume = newVolume;
        _audioMixer.SetFloat("MainVolume", _mainVolume);
    }


    private void ToggleBGM(int val)
    {
        if (Mathf.RoundToInt(val) == 1)
        {
             PlayBGMLoop(_currentBackgroundMusic);
        }
        else
        {
            StopPlayingBGMLoop();
        }
    }
    #endregion

    public void PlayOnceUI(int clipNo)
    {
        if (clipNo >= sources.clipsUI.Length)
        {
            Debug.LogError("Clip not in array cannot play sound");
            return;
        }
        _uiSource.PlayOneShot(sources.clipsUI[clipNo]);

    }

    public void PlayOnceSFX(int clipNo)
    {
        if (clipNo >= sources.soundFX.Length)
        {
            Debug.LogError("Clip not in array cannot play sound");
            return;
        }

        foreach (AudioSource audio in _playOnceSourcePoolFX)
        {
            if (!audio.isPlaying)
            {
                audio.PlayOneShot(sources.soundFX[clipNo]);
                return;
            }

        }
        AddSourceToPlayOncePool();
        _playOnceSourcePoolFX[_playOncePoolSize].PlayOneShot(sources.soundFX[clipNo]);
        _playOncePoolSize += 1;
    }

    public void PlayBGMLoop(int clipNo)
       {
           if (!_bgmSource.isPlaying)
           {
            _bgmSource.loop = true;
            _bgmSource.clip = sources.bgmClips[clipNo];
            _bgmSource.Play();
           }
           else if (_bgmSource.clip != sources.bgmClips[clipNo])
           {
            _bgmSource.Stop();
            _bgmSource.loop = true;
            _bgmSource.clip = sources.bgmClips[clipNo];
            _bgmSource.Play();
           }

       }


       public void StopPlayingBGMLoop()
       {
        _bgmSource.Stop();
       }


    public AudioSource StartPlayingLoopedSource(int clipNo)
    {
        if (clipNo >= sources.soundFX.Length)
        {
            Debug.LogError("Clip not in array cannot play sound");
            return null;
        }

        AudioSource temp = null;
        foreach (AudioSource audio in _playOnceSourcePoolFX)
        {
            if (!audio.isPlaying)
            {
                audio.loop = true;
                audio.clip = sources.soundFX[clipNo];
                audio.Play();
                temp = audio;

                return temp;
            }

        }

        AddSourceToPlayOncePool();
        temp = _playOnceSourcePoolFX[_playOncePoolSize];
        temp.loop = true;
        temp.clip = sources.soundFX[clipNo];
        temp.Play();
        _playOncePoolSize += 1;

        return temp;
    }

    public void StopLoopedSource(AudioSource source)
    {
        source.Stop();
        source.loop = false;
        source.clip = null;
    }

    private bool CanPlay()
    {
        //add code check if clip can be played
        return true;
    }

    private void SoundToggled(GameResources.volumeType type, bool on)
    {
        if(type == GameResources.volumeType.BGM)
        {
            if (!on)
            {
                StopPlayingBGMLoop();
            }
            else
            {
                PlayBGMLoop(0);
            }
        }else if(type == GameResources.volumeType.MASTER)
        {
            if (!on)
            {
                previousMasterVolum = mainVolume;
                ChangeMainVolume(-80);
            }
            else
            {
                ChangeMainVolume(previousMasterVolum);
            }
        }
    
    }


}
