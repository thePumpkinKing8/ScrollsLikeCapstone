using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class BlakesAudioManager : Singleton<BlakesAudioManager>
{
    // One scriptable object per clip
    // To add a new sound, create a scriptable object using the AudioSO class and assign the values to it, then add it to the _audioSOs list in the inspector of the AudioManager object in the scene.
    // Make sure to assign sfx to an sfx mixer group and music to a music mixer group



    private AudioSO[] _audioSOs;

    private AudioSource _audioSource;

    [SerializeField] private AudioSource _musicSource;

    private Dictionary<string, AudioSO> _audioPairs;

    private List<AudioSource> _audioPool;

    private int _poolSize;


    protected override void Awake()
    {
       base.Awake();
        DontDestroyOnLoad(gameObject);
        _audioSOs = Resources.LoadAll<AudioSO>("AudioSOs");
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioPairs = new Dictionary<string, AudioSO>();
        _poolSize = _audioSOs.Length; // Amount of audio sources we will instantiate depends on how many clips we have



        foreach (var audioData in _audioSOs)
        {
            // Populate dictionary
            if (!_audioPairs.ContainsKey(audioData.AudioName))
            {
                _audioPairs.Add(audioData.AudioName, audioData);
            }
        }


       

        _audioPool = new List<AudioSource>();

        for (int i = 0; i < _poolSize; i++)
        {
            AudioSource pooledAudioSource = this.gameObject.AddComponent<AudioSource>();
            pooledAudioSource.playOnAwake = false;
            _audioPool.Add(pooledAudioSource);
        }

        /*
        //subscribes to all level events that play audio
        LevelEventData data = LevelManager.Instance.EventData;
        List<UnityEvent<string>> events = new List<UnityEvent<string>>();

        foreach (FieldInfo info in data.GetType().GetFields())
        {
            if (info.GetValue(data) as UnityEvent<string> != null)
            {
                events.Add(info.GetValue(data) as UnityEvent<string>);
            }
        }
        foreach (UnityEvent<string> unityEvent in events)
        {
            unityEvent.AddListener(PlayAudio);
        }
        */
    }

    public AudioSource GetAudioSource()
    {
        // Gets an available audio source out of the pool
        foreach (AudioSource audioSource in _audioPool)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        return null;
    }

    public void PlayAudio(string audioName)
    {
        if (_audioPairs.TryGetValue(audioName, out var audioData))
        {
            _audioSource = GetAudioSource();

            // Gives the parameters from the scriptable object to the audio clip in question
            _audioSource.clip = audioData.Clip;
            _audioSource.volume = audioData.Volume;
            _audioSource.pitch = audioData.Pitch;
            _audioSource.priority = audioData.Priority;
            _audioSource.loop = audioData.Loop;
            _audioSource.playOnAwake = audioData.PlayOnAwake;
            _audioSource.outputAudioMixerGroup = audioData.Mixer;
            audioData.PlaySource = _audioSource;
            _audioSource.PlayOneShot(audioData.Clip);

        }
        else
        {
            Debug.Log("The clip" + audioName + "cannot be found.");
        }
    }

    public void PlayMusic(string audioName)
    {
        if (_audioPairs.TryGetValue(audioName, out var audioData))
        {
            if (audioData.PlaySource != null)//prevents same music from being restarted repeatedly
            {
                if (audioData.PlaySource.clip == audioData.Clip)
                {
                    return;
                }
            }

            StopMusic();
            _audioSource = _musicSource;

            // Gives the parameters from the scriptable object to the audio clip in question
            _audioSource.clip = audioData.Clip;
            _audioSource.volume = audioData.Volume;
            _audioSource.pitch = audioData.Pitch;
            _audioSource.priority = audioData.Priority;
            _audioSource.loop = true;
            _audioSource.playOnAwake = audioData.PlayOnAwake;
            _audioSource.outputAudioMixerGroup = audioData.Mixer;
            audioData.PlaySource = _audioSource;

            _audioSource.clip = audioData.Clip;
            _audioSource.Play();

        }
        else
        {
            Debug.Log("The clip" + audioName + "cannot be found.");
        }
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource.UnPause();
    }

    public void PauseAudio(string audioName)
    {
        _audioSource.Pause();
    }

    public void UnPauseAudio(string audioName)
    {
        _audioSource.UnPause();
    }

    public void StopAudio(string audioName)
    {
        if (_audioPairs.TryGetValue(audioName, out var audioData))
        {
            if (audioData.PlaySource != null)
            {
                if (audioData.PlaySource.clip == audioData.Clip)
                {
                    audioData.PlaySource.Stop();
                }
            }
        }
    }

}