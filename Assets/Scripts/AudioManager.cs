using UnityEngine;

/// <summary>
/// 모든 오디오 관리
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// sfx 종류
    /// </summary>
    public enum Sfx {
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

    #region Variable
    public static AudioManager instance;
    [Header("BGM")]
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private float bgmVolume;
    private AudioSource bgmPlayer;
    private AudioHighPassFilter bgmFilter;

    [Header("SFX")]
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private float sfxVolume;
    private int chanels = 10;
    private AudioSource[] sfxPlayers;

    #endregion

    #region InitMethod

    private void Awake()
    {
        //싱글톤
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        bgmFilter = Camera.main.GetComponent<AudioHighPassFilter>();
        bgmFilter.enabled = false;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
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
    #endregion

    #region SetAudioMethod

    /// <summary>
    /// bgm 필터 적용 여부
    /// </summary>
    /// <param name="islive"></param>
    public void OnEffect(bool islive)
    {
        bgmFilter.enabled = islive;
    }

    /// <summary>
    /// sfx 재생
    /// </summary>
    /// <param name="type"></param>
    public void PlaySfx(Sfx type)
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

    /// <summary>
    /// bgm 재생 및 정지
    /// </summary>
    /// <param name="islive">재생 여부</param>
    public void PlayBgm(bool islive)
    {
        if (islive)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();
    }
    #endregion
}
