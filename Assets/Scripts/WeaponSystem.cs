using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("生成間隔"), Range(0, 10)]
    public float interval = 3.5f;
    [Header("武器攻擊"), Range(0, 10000)]
    public float attack = 50;
    [Header("武器預製物")]
    public GameObject prefabWeapon;
    [Header("武器的推力")]
    public Vector2 power;

    private void SpawnWeapon()
    {
        GameObject tempWeapon = Instantiate(prefabWeapon, transform.position, transform.rotation);
        
        Rigidbody2D rigWeapon = tempWeapon.GetComponent<Rigidbody2D>();

        rigWeapon.AddForce(power * transform.right + new Vector2(0, power.y));

        tempWeapon.GetComponent<Weapon>().attack = this.attack;

        AudioClip sound = SoundManager.instance.soundFireWeapon;
        SoundManager.instance.PlaySound(sound, 0.8f, 1.5f);
    }

    private void Awake()
    {
        InvokeRepeating("SpawnWeapon", 0, interval);
    }

    public void Restart()
    {
        CancelInvoke("SpawnWeapon");
        InvokeRepeating("SpawnWeapon", 0, interval);
    }

    public void Stop()
    {
        CancelInvoke("SpawnWeapon");
    }
}
