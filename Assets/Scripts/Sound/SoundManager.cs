using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tutorial : https://www.youtube.com/watch?v=QL29aTa7J5Q
/// </summary>
public class SoundManager
{
    public enum Sound
    {
        LoadSprint,
        UnloadSprint,
        LaunchFireBall,
        Hit,
        Win
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.maxDistance = 100f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.Play();
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
       audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClips)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioclip;
            }
        }

        Debug.LogError("Sound " + sound + " not found !");
        return null;
    }
}
