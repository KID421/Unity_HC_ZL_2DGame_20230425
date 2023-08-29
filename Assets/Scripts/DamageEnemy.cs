using UnityEngine;
using UnityEngine.Events;

public class DamageEnemy : DamageBasic
{
    [Header("死亡事件")]
    public UnityEvent onDead;

    private DataEnemy dataEnemy;
    private DamagePlayer damagePlayer;

    private void Start()
    {
        dataEnemy = (DataEnemy)data;
        // print(dataEnemy.expProbability);
        
        damagePlayer = GameObject.Find("爆走企鵝").GetComponent<DamagePlayer>();
        
        if (name.Contains("BOSS")) onDead.AddListener(() => damagePlayer.Win());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("武器"))
        {
            float damage = collision.gameObject.GetComponent<Weapon>().attack;
            Damage(damage);
            AudioClip sound = SoundManager.instance.soundEnemyHurt;
            SoundManager.instance.PlaySound(sound, 0.8f, 1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("武器"))
        {
            float damage = collision.gameObject.GetComponent<Weapon>().attack;
            Damage(damage);
        }
    }

    protected override void Dead()
    {
        base.Dead();

        AudioClip sound = SoundManager.instance.soundEnemyDead;
        SoundManager.instance.PlaySound(sound, 0.8f, 1.5f);

        onDead.Invoke();
        Destroy(gameObject);

        // print("隨機值：" + Random.value);

        if (Random.value < dataEnemy.expProbability)
        {
            Instantiate(dataEnemy.prefabExp, transform.position, transform.rotation);
        }
    }
}