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

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }
}
