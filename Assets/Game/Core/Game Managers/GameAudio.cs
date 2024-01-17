using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public static GameAudio Instance;
    [SerializeField]
    private List<AudioSource> _ambientSoundSources;
    [SerializeField]
    private List<AudioSource> _stereoSoundSources;
    [SerializeField]
    private List<AudioSource> _spatialSoundSources;

    private int _currentAmbientSoundSourcesIndex;
    private int _currentStereoSoundSourcesIndex;
    private int _currentSpatialSoundSourcesIndex;

    private void Awake()
    {
        Instance = this;
    }

    public static void PlayEffectAudioAtPosition(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (Instance._spatialSoundSources == null || Instance._spatialSoundSources.Count == 0) return;

        if (++Instance._currentSpatialSoundSourcesIndex == Instance._spatialSoundSources.Count)
            Instance._currentSpatialSoundSourcesIndex = 0;

        AudioSource source = Instance._spatialSoundSources[Instance._currentSpatialSoundSourcesIndex];
        source.transform.position = position;
        source.PlayOneShot(clip, volume);
    }

    public static void PlayEffectAudio(AudioClip clip, float volume = 1f)
    {
        if (Instance._stereoSoundSources == null || Instance._stereoSoundSources.Count == 0) return;

        if(++Instance._currentStereoSoundSourcesIndex == Instance._stereoSoundSources.Count)
            Instance._currentStereoSoundSourcesIndex = 0;

        AudioSource source = Instance._stereoSoundSources[Instance._currentStereoSoundSourcesIndex];
        source.PlayOneShot(clip,volume);
    }

    public static void PlayAmbienceAudio(AudioClip clip, float volume = 1f, bool loop = false)
    {
        if(Instance._ambientSoundSources == null || Instance._ambientSoundSources.Count == 0) return; 

        if(++Instance._currentAmbientSoundSourcesIndex == Instance._ambientSoundSources.Count)
            Instance._currentAmbientSoundSourcesIndex = 0;

        AudioSource source = Instance._ambientSoundSources[Instance._currentAmbientSoundSourcesIndex];
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }

    public static void StopAllSounds()
    {
        Instance._ambientSoundSources.ForEach(x => x.Stop());
        Instance._stereoSoundSources.ForEach(x => x.Stop());
        Instance._spatialSoundSources.ForEach(x => x.Stop());
    }
}
