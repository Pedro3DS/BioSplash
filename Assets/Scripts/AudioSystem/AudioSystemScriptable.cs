using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public struct AudioClipData
{
    public string Name;
    public AudioClip Clip;
    public bool PlayOnAwake;
    public bool Loop;
    [Header("Audio Settings")]

    [Tooltip("Audio Mixer Group to route the audio through")]

    [Header("Audio Priority: 0 (highest) to 256 (lowest)")]
    [SerializeField] [Range(0f,256f), DefaultValue(128f)] public float Priority;
    [Header("Audio Volume: 0 (silent) to 1 (full volume)")]
    [SerializeField] [Range(0f,1f), DefaultValue(1f)] public float Volume;
    [Header("Audio Pitch: -3 (three octaves down) to 3 (three octaves up)")]
    [SerializeField] [Range(-3f,3f), DefaultValue(1f)] public float Pitch;
    [Header("Audio Stereo Pan: -1 (left) to 1 (right)")]
    [SerializeField] [Range(-1f,1f), DefaultValue(0f)] public float StereoPan;
    [Header("Audio Spatial Blend: 0 (2D) to 1 (3D)")]
    [SerializeField] [Range(0f,1f), DefaultValue(0f)] public float SpatialBlend;
    [Header("Audio Reverb Zone Mix: 0 (no reverb) to 1.1 (max reverb)")]
    [SerializeField] [Range(0f,1.1f), DefaultValue(1f)] public float ReverbZoneMix;

}

[CreateAssetMenu(fileName = "AudioSystemScriptable", menuName = "ScriptableObjects/AudioSystemScriptable", order = 1)]
public class AudioSystemScriptable : ScriptableObject
{
    [Header("Audio Clips")]

    [Header("Music Data Clips")]
    public AudioClipData[] AudiosDatas;

    [Header("SFX Data Clips")]
    public AudioClipData[] SFXDatas;

    [Header("Ambient Data Clips")]
    public AudioClipData[] AmbientDatas;

    [Header("UI Data Clips")]
    public AudioClipData[] UIDatas;

    [Header("Voice Data Clips")]
    public AudioClipData[] VoiceDatas;

    [Header("DistantLocations Data Clips")]
    public AudioClipData[] DistantLocationsDatas;


}
