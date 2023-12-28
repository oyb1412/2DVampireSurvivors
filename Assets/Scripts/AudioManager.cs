using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    public AudioSource bgmPlayer;
    public AudioHighPassFilter bgmFilter;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int chanels;
    public AudioSource[] sfxPlayers;

    public enum Sfx
    {
        Dead,
        Hit,
        LevelUp,
        Lose,
        Select,
        Victory,
        PlayerHit,
        Charge,
        Sword
    }
    private void Awake()
    {
        instance = this;
        bgmFilter = Camera.main.GetComponent<AudioHighPassFilter>();
        bgmFilter.enabled = false;
    }
    private void Start()
    {
        Init();
    }
    void Init()
    {
        //BGM
        GameObject bgmObj = new GameObject("BGMPlayer");
        bgmObj.transform.parent = transform;
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        //SFX
        GameObject sfxObj = new GameObject("SFXPlayer");
        sfxObj.transform.parent = transform;
        sfxPlayers = new AudioSource[chanels];
        for(int i = 0;i <sfxPlayers.Length;i++)
        {
            sfxPlayers[i] = sfxObj.AddComponent<AudioSource>();
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].bypassListenerEffects = false;
 

        }
    }

    public void OnEffect(bool islive)
    {
        bgmFilter.enabled = islive;
    }

    public void PlayerSfx(Sfx type)
    {
        for(int i = 0; i < sfxPlayers.Length;i++)
        {
            if (sfxPlayers[i].isPlaying)
                continue;

            sfxPlayers[i].clip = sfxClips[(int)type];
            sfxPlayers[i].Play();
            break;
        }
    }

    public void PlayerBgm(bool islive)
    {
        if (islive)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();
    }
}
