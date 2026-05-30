using System.Collections;
using UnityEngine;

/// <summary>
/// AudioController is a singleton that manages audio playback in the game. It holds a reference to an AudioSystemScriptable which contains the audio clip data and settings. The AudioController can be accessed globally to play audio clips based on the data defined in the AudioSystemScriptable. It also ensures that only one instance of the AudioController exists throughout the game and persists across scene loads.
/// </summary>

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [SerializeField] private AudioSystemScriptable audioSystemScriptable;

    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceSFX;
    [SerializeField] private AudioSource audioSourceAmbient;
    [SerializeField] private AudioSource audioSourceUI;
    [SerializeField] private AudioSource audioSourceVoice;
    [SerializeField] private AudioSource audioSourceDistanteLocations;

    public AudioSystemScriptable AudioSystemScriptable => audioSystemScriptable;

    //AudiosSources

    public AudioSource AudioSourceMusic => audioSourceMusic;
    public AudioSource AudioSourceSFX => audioSourceSFX;
    public AudioSource AudioSourceAmbient => audioSourceAmbient;
    public AudioSource AudioSourceUI => audioSourceUI;
    public AudioSource AudioSourceVoice => audioSourceVoice;
    public AudioSource AudioSourceDistanteLocations => audioSourceDistanteLocations;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    


    public void PlayAudio(string audioName)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            AudioSource source = GetAudioSourceForClip(data);
            if (source != null)
            {
                source.clip = data.Clip;
                source.volume = data.Volume;
                source.pitch = data.Pitch;
                source.panStereo = data.StereoPan;
                source.spatialBlend = data.SpatialBlend;
                source.reverbZoneMix = data.ReverbZoneMix;
                source.priority = (int)data.Priority;
                source.loop = data.Loop;
                if (data.PlayOnAwake)
                {
                    source.Play();
                }
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    public void StopAudio(string audioName)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            AudioSource source = GetAudioSourceForClip(data);
            if (source != null && source.clip == data.Clip)
            {
                source.Stop();
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    public void PauseAudio(string audioName)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            AudioSource source = GetAudioSourceForClip(data);
            if (source != null && source.clip == data.Clip)
            {
                source.Pause();
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    public void ResumeAudio(string audioName)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            AudioSource source = GetAudioSourceForClip(data);
            if (source != null && source.clip == data.Clip)
            {
                source.UnPause();
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    public void PlayOneShotAudio(string audioName)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            AudioSource source = GetAudioSourceForClip(data);
            if (source != null)
            {
                source.PlayOneShot(data.Clip, data.Volume);
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    public void PlayDistancedAudio(string audioName, Vector3 position)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            if (data.SpatialBlend > 0.5f) // Ensure it's a 3D sound
            {
                AudioSource.PlayClipAtPoint(data.Clip, position, data.Volume);
            }
            else
            {
                Debug.LogWarning($"Audio clip with name '{audioName}' is not set as a 3D sound in AudioSystemScriptable.");
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    public void SoftAudioTransition(string audioName, float fadeDuration)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            AudioSource source = GetAudioSourceForClip(data);
            if (source != null)
            {
                StartCoroutine(FadeAudio(source, data.Clip, data.Volume, fadeDuration));
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    // public void FadeAudio(AudioSource source, AudioClip newClip, float targetVolume, float duration)
    // {
    //     StartCoroutine(FadeAudioCoroutine(source, newClip, targetVolume, duration));
    // }

    public IEnumerator FadeAudio(AudioSource source, AudioClip newClip, float targetVolume, float duration)
    {
        float startVolume = source.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        source.clip = newClip;
        source.Play();
    }

    public IEnumerator FadeOutAudio(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // Reset volume for next play
    }

    public void StopAudioWithFade(string audioName, float fadeDuration)
    {
        AudioClipData? audioData = GetAudioClipData(audioName);
        if (audioData.HasValue)
        {
            AudioClipData data = audioData.Value;
            AudioSource source = GetAudioSourceForClip(data);
            if (source != null && source.clip == data.Clip)
            {
                StartCoroutine(FadeOutAudio(source, fadeDuration));
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip with name '{audioName}' not found in AudioSystemScriptable.");
        }
    }

    public void StopAllAudioOfType(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void StopAllAudio()
    {
        audioSourceMusic.Stop();
        audioSourceSFX.Stop();
        audioSourceAmbient.Stop();
        audioSourceUI.Stop();
        audioSourceVoice.Stop();
        audioSourceDistanteLocations.Stop();
    }

    public AudioClipData? GetAudioClipData(string audioName)
    {
        foreach (AudioClipData audioData in audioSystemScriptable.AudiosDatas)
        {
            if (audioData.Name == audioName)
            {
                return audioData;
            }
        }
        return null;
    }

    public AudioSource GetAudioSourceForClip(AudioClipData audioData)
    {
        // This is a simple example. You can expand this logic to route audio clips to different sources based on your needs.
        if (audioData.SpatialBlend > 0.5f)
        {
            return audioSourceAmbient; // For 3D sounds
        }
        else
        {
            return audioSourceSFX; // For 2D sounds
        }
    }


}
