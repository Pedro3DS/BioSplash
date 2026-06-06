// Author: Gabriel Barberiz - https://gabrielbarberiz.notion.site/
// Created: 2025/01/06

using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace NEO.Audio
{
    /// <summary>
    /// A static utility class for handling advanced audio playback and effects using Unity's AudioSource and DOTween.
    /// Provides methods for playing, fading, and crossfading audio, as well as callbacks for when audio ends.
    /// </summary>
    public static class NEOAudio
    {
        #region Consts
        private const float FADE_IN_DURATION = 10f;
        private const float FADE_OUT_DURATION = 5f;
        private const float MIN_RANDOM_PITCH = 0.9f;
        private const float MAX_RANDOM_PITCH = 1.1f;
        private const float LOG_TO_DECIBEL = 20f;
        private const float DECIBEL_TO_LOG = 0.05f;
        #endregion

        #region AudioSource

        #region Playback

        /// <summary>
        /// Plays an AudioSource and applies a fade-in effect.
        /// </summary>
        /// <param name="audioSource">The AudioSource to play.</param>
        public static void NEOPlay(this AudioSource audioSource, float fadeInDuration = FADE_IN_DURATION)
        {
            if (audioSource == null || audioSource.clip == null) return;

            audioSource.Play();
            audioSource.NEOFadeIn(fadeInDuration);
        }

        /// <summary>
        /// Plays an AudioSource with a randomized pitch for natural variation.
        /// </summary>
        /// <param name="audioSource">The AudioSource to play.</param>
        /// <param name="minPitch">The minimum pitch value for randomization.</param>
        /// <param name="maxPitch">The maximum pitch value for randomization.</param>
        public static void NEOPlayWithRandomizePitch(this AudioSource audioSource, float fadeInDuration = FADE_IN_DURATION, float minPitch = MIN_RANDOM_PITCH, float maxPitch = MAX_RANDOM_PITCH)
        {
            if (audioSource == null || audioSource.clip == null) return;

            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
            audioSource.NEOFadeIn(fadeInDuration);
        }

        /// <summary>
        /// Gradually decreases the volume of an AudioSource over a specified duration.
        /// </summary>
        /// <param name="audioSource">The AudioSource to fade out.</param>
        /// <param name="duration">The duration of the fade-out effect. Default is FADE_DURATION.</param>
        public static void NEOStop(this AudioSource audioSource, float duration = FADE_OUT_DURATION)
        {
            if (audioSource == null) return;

            DOTween.Kill(audioSource);
            audioSource.DOFade(0f, duration).SetEase(Ease.Linear).OnComplete(() => audioSource.Stop());
        }
        #endregion

        #region FadeEffects

        /// <summary>
        /// Gradually increases the volume of an AudioSource over a specified duration.
        /// </summary>
        /// <param name="audioSource">The AudioSource to fade in.</param>
        /// <param name="duration">The duration of the fade-in effect. Default is FADE_DURATION.</param>
        public static void NEOFadeIn(this AudioSource audioSource, float duration = FADE_IN_DURATION)
        {
            if (audioSource == null || audioSource.clip == null) return;

            float targetVolume = Mathf.Clamp01(audioSource.volume);
            audioSource.volume = 0;
            audioSource.DOFade(targetVolume, duration).SetEase(Ease.Linear);
        }

        #endregion

        #endregion

        #region AudioMixer
        /// <summary>
        /// Sets the volume of an AudioMixer group using a linear scale (0 to 1).
        /// </summary>
        /// <param name="mixer">The AudioMixer instance.</param>
        /// <param name="parameterName">The exposed parameter name (e.g., "MasterVolume").</param>
        /// <param name="volume">The target volume as a linear value (0 to 1).</param>
        public static void NEOSetVolume(this AudioMixer mixer, string parameterName, float volume)
        {
            if (mixer == null)
            {
                Debug.LogWarning("AudioMixer is null.");
                return;
            }

            float clampedVolume = Mathf.Clamp(volume, 0.0001f, 1f);

            float decibelVolume = LOG_TO_DECIBEL * Mathf.Log10(clampedVolume);

            if (!mixer.SetFloat(parameterName, decibelVolume))
            {
                Debug.LogWarning($"Parameter: \"{parameterName}\" is not a exposed paramenter");
            }
        }
        /// <summary>
        /// Gets the current volume (linear scale) of an exposed AudioMixer parameter.
        /// </summary>
        /// <param name="mixer">The AudioMixer instance.</param>
        /// <param name="parameterName">The name of the exposed parameter.</param>
        /// <param name="volume">The retrieved volume in linear scale (0.0001 to 1).</param>
        /// <returns>True if the parameter exists and the volume is retrieved; false otherwise.</returns>
        public static bool NEOGetVolume(this AudioMixer mixer, string parameterName, out float volume)
        {
            volume = 0f;

            if (mixer == null)
            {
                Debug.LogWarning("AudioMixer is null.");
                return false;
            }

            if (mixer.GetFloat(parameterName, out float decibelVolume))
            {
                // Convert decibel volume back to linear scale
                volume = Mathf.Pow(10f, decibelVolume * DECIBEL_TO_LOG);
                volume = Mathf.Clamp(volume, 0.0001f, 1f); // Clamp to avoid extreme values
                return true;
            }

            Debug.LogWarning($"Parameter: \"{parameterName}\" is not an exposed parameter.");
            return false;
        }
        #endregion
    }
}
