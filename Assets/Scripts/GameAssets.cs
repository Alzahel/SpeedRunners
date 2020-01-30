using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// tutorial /https://www.youtube.com/watch?v=7GcEW6uwO8E
/// </summary>

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets Instance
    {
        get
        {
            if(_i==null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    /*public Sprite freeze;
    public Sprite heal;
    public Sprite shrink;*/

    public SpriteAsset[] sprites;
    [System.Serializable]
    public class SpriteAsset
    {
        public string name;
        public Sprite sprite;
    }

    public SoundAudioClip[] soundAudioClips;
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioclip;
    }

}
