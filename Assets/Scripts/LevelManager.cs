using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("經驗值")]
    public Image imgExp;
    [Header("文字等級")]
    public TextMeshProUGUI textLv;
    [Header("文字經驗值")]
    public TextMeshProUGUI textExp;
    [Header("升級面板")]
    public GameObject goLvUp;
    [Header("技能 1~3")]
    public GameObject[] goSkillUI;

    /// <summary>
    /// 0 吸取經驗
    /// 1 啤酒攻擊
    /// 2 啤酒間隔
    /// 3 玩家血量
    /// 4 移動速度
    /// </summary>
    [Header("技能資料陣列")]
    public DataSkill[] dataSkills;

    public List<DataSkill> randomSkill = new List<DataSkill>();

    private int lv = 1;
    private float exp = 0;

    public float[] expNeeds = { 100, 200, 300 };

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            // print($"<color=#ff6699>i 的值：{i}</color>");
        }

        for (int i = 0; i < dataSkills.Length; i++) dataSkills[i].skillLv = 1;

        UpdateExpRange();
        UpdateBeerAttack();
        UpdateBeerInterval();
        UpdatePlayerHp();
        UpdateMoveSpeed();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1)) AddExp(100);
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // print($"<color=#6699ff>{collision.name} </color>");

        if (collision.name.Contains("經驗值"))
        {
            collision.GetComponent<ExpSystem>().enabled = true;
        }
    }

    public void AddExp(float exp)
    {
        this.exp += exp;

        while (this.exp >= expNeeds[lv - 1])
        {
            this.exp -= expNeeds[lv - 1];
            lv++;
            textLv.text = lv.ToString();
            LevelUp();
        }

        textExp.text = this.exp + " / " + expNeeds[lv - 1];
        imgExp.fillAmount = this.exp / expNeeds[lv - 1];
    }

    [Header("關閉")]
    public GameObject btnClose;

    private void LevelUp()
    {
        AudioClip sound = SoundManager.instance.soundLvUp;
        SoundManager.instance.PlaySound(sound, 0.8f, 1.5f);

        goLvUp.SetActive(true);
        Time.timeScale = 0;

        randomSkill = dataSkills.Where(skill => skill.skillLv < 10).ToList();
        randomSkill = randomSkill.OrderBy(skill => Random.Range(0, 999)).ToList();

        for (int i = 0; i < 3; i++)
        {
            if (i > randomSkill.Count - 1)
            {
                goSkillUI[i].SetActive(false);
            }
            else
            {
                goSkillUI[i].transform.Find("技能名稱").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillName;
                goSkillUI[i].transform.Find("技能描述").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillDescription;
                goSkillUI[i].transform.Find("技能等級").GetComponent<TextMeshProUGUI>().text = "Lv" + randomSkill[i].skillLv;
                goSkillUI[i].transform.Find("技能圖片").GetComponent<Image>().sprite = randomSkill[i].skillPicture;
            }
        }

        if (randomSkill.Count == 0) btnClose.SetActive(true);
    }

    [ContextMenu("產生經驗值需求資料")]
    private void ExpNeeds()
    {
        expNeeds = new float[100];

        for (int i = 0; i < 100; i++)
        {
            expNeeds[i] = (i + 1) * 100;
        }
    }

    public void ClickCloseButton()
    {
        goLvUp.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClickSkillButton(int indexSkill)
    {
        AudioClip sound = SoundManager.instance.soundSkillLvUp;
        SoundManager.instance.PlaySound(sound, 0.8f, 1.5f);

        // print($"<color=#6699ff>點擊技能編號：{indexSkill}</color>");
        randomSkill[indexSkill].skillLv++;
        goLvUp.SetActive(false);
        Time.timeScale = 1;

        if (randomSkill[indexSkill].skillName == "吸取經驗值範圍") UpdateExpRange();
        if (randomSkill[indexSkill].skillName == "武器啤酒攻擊") UpdateBeerAttack();
        if (randomSkill[indexSkill].skillName == "武器生成間隔") UpdateBeerInterval();
        if (randomSkill[indexSkill].skillName == "玩家血量") UpdatePlayerHp();
        if (randomSkill[indexSkill].skillName == "移動速度") UpdateMoveSpeed();
    }

    [Header("爆走企鵝：圓形碰撞")]
    public CircleCollider2D playerExpRange;

    private void UpdateExpRange()
    {
        int lv = dataSkills[0].skillLv - 1;
        playerExpRange.radius = dataSkills[0].skillValues[lv];
    }

    [Header("武器啤酒生成點")]
    public WeaponSystem weaponSystemBeer;

    private void UpdateBeerAttack()
    {
        int lv = dataSkills[1].skillLv - 1;
        weaponSystemBeer.attack = dataSkills[1].skillValues[lv];
    }

    private void UpdateBeerInterval()
    {
        int lv = dataSkills[2].skillLv - 1;
        weaponSystemBeer.interval = dataSkills[2].skillValues[lv];
        weaponSystemBeer.Restart();
    }

    [Header("玩家資料")]
    public DataBasic dataBasic;
    [Header("玩家受傷")]
    public DamagePlayer damagePlayer;

    private void UpdatePlayerHp()
    {
        int lv = dataSkills[3].skillLv - 1;
        dataBasic.hp = dataSkills[3].skillValues[lv];
        damagePlayer.LevelUp();
    }

    [Header("爆走企鵝：控制系統")]
    public ControlSystem controlSystem;

    private void UpdateMoveSpeed()
    {
        int lv = dataSkills[4].skillLv - 1;
        controlSystem.moveSpeed = dataSkills[4].skillValues[lv];
    }
}
