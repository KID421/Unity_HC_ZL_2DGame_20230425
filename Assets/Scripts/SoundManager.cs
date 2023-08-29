using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("升級")]
    public AudioClip soundLvUp;
    [Header("升級技能")]
    public AudioClip soundSkillLvUp;
    [Header("玩家受傷")]
    public AudioClip soundPlayerHurt;
    [Header("玩家死亡")]
    public AudioClip soundPlayerDead;
    [Header("怪物受傷")]
    public AudioClip soundEnemyHurt;
    [Header("怪物死亡")]
    public AudioClip soundEnemyDead;
    [Header("發射武器")]
    public AudioClip soundFireWeapon;

    private AudioSource aud;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放聲音
    /// </summary>
    /// <param name="sound">要播放的音效</param>
    /// <param name="min">音量最小值</param>
    /// <param name="max">音量最大值</param>
    public void PlaySound(AudioClip sound, float min = 0.7f, float max = 1.2f)
    {
        float random = Random.Range(min, max);
        aud.PlayOneShot(sound, random);
    }
}
