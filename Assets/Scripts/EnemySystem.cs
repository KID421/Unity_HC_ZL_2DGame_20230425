using System.Collections;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [Header("敵人資料")]
    public DataEnemy data;

    private Transform player;
    private float timer;
    private DamagePlayer damagePlayer;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0.3f, 0.5f);

        Gizmos.DrawSphere(transform.position, data.attackRange);
    }

    private void Awake()
    {
        player = GameObject.Find("爆走企鵝").transform;
        damagePlayer = player.GetComponent<DamagePlayer>();
        timer = data.attackInterval;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        // print($"<color=#af9>距離：{distance}</color>");

        if (distance > data.attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, data.moveSpeed * Time.deltaTime);
        }
        else
        {
            // print("<color=#f96>進入攻擊範圍</color>");

            timer += Time.deltaTime;
            // print($"<color=#9f4>計時器：{timer}</color>");

            if (timer >= data.attackInterval)
            {
                timer = 0;
                StartCoroutine(AttackEffect());
            }
        }

        if (transform.position.x > player.position.x) transform.eulerAngles = new Vector3(0, 0, 0);
        else transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private IEnumerator AttackEffect()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.localScale += Vector3.one * 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        damagePlayer.Damage(data.attack);

        for (int i = 0; i < 10; i++)
        {
            transform.localScale -= Vector3.one * 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
