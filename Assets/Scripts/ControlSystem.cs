using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    [Header("移動速度")]
    [Range(0, 100)]
    public float moveSpeed = 3.5f;
    [Header("剛體")]
    public Rigidbody2D rig;
    [Header("動畫控制器")]
    public Animator ani;
    [Header("跑步參數")]
    public string parRun = "開關走路";

    private void Awake()
    {
        // print(666);
        // print("嗨~");
        // print("1 + 2");
        // print($"{1 + 2}");
    }

    private void Start()
    {
        // print("<color=yellow>開始事件</color>");
    }

    private void Update()
    {
        // print("<color=#998877>更新事件</color>");

        // 呼叫移動方法
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rig.velocity = new Vector2(h, v) * moveSpeed;

        // h 不等於 0 或者 v 不等於 0 要走路
        ani.SetBool(parRun, h != 0 || v != 0);

        // print(Input.GetKeyDown(KeyCode.A));

        // 如果 按下 A 或者 按下 左 就 設定為 180
        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        if (h < 0)
        {
            // print("玩家按下 A");
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // 如果 按下 D 或者 按下 右 就 設定為 0
        //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        if (h > 0)
        {
            // print("玩家按下 D");
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
