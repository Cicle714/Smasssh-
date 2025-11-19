using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 m_Position1; //タッチした場所
    private Vector3 m_Position2; //引っ張る方向
    [SerializeField]
    private Vector3 playerVec; //プレイヤーのベクトル

    [SerializeField]
    private GameObject Arrow;　//移動する方向の可視化

    public float debug;

    private bool isAttack = false; //自分が攻撃したか

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Position1 = Input.mousePosition;　//左クリックで位置取得
            Arrow.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_Position1.z = Camera.main.transform.position.y;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(m_Position1); //左クリックの位置をゲーム画面の位置に変換
            Arrow.SetActive(false);　//矢印を見えなくする
            m_Position2 = Input.mousePosition;　//左クリックを離した位置を取得
            playerVec = m_Position1 - m_Position2;　//プレイヤーのベクトルの計算
            if (playerVec.magnitude > 500)
                playerVec = playerVec.normalized * 500;　//パワーが500以上なら500に制限
            rb.linearVelocity = new Vector3(playerVec.x, 0, playerVec.y) * 0.05f;　//プレイヤーが動く処理
            Vector3 rote = Vector3.up * (Mathf.Abs((transform.position.x - worldPos.x) + (transform.position.z - worldPos.z))) * (playerVec.magnitude) / 5.0f;
            if (rote.magnitude > 500)
                rote = rote.normalized * 500;
            
            rb.AddTorque(rote);　//回転の力を加える
            Debug.Log(worldPos);
            isAttack = true; //攻撃した
        }

        
        if (Arrow.activeSelf)
        {
            m_Position2 = Input.mousePosition;　//マウスの位置を取得　
            playerVec = m_Position1 - m_Position2;　//プレイヤーのベクトル
            if (playerVec.magnitude > 500)
                playerVec = playerVec.normalized * 500;　//プレイヤーのベクトルの強さ制限
            Arrow.transform.localScale = new Vector3(1, playerVec.magnitude / 200); //ベクトルの強さで矢印の大きさを変える
            Arrow.transform.position = transform.position;　　//矢印の位置をプレイヤーにする
            Arrow.transform.rotation = Quaternion.Euler(90, 0, (Mathf.Atan2(playerVec.y, playerVec.x) * 180.0f / Mathf.PI) - 90);　//ベクトルの向きに矢印を回転させる
        }
        if (rb.linearVelocity == Vector3.zero && isAttack)
        {
            FindObjectOfType<GameManager>().myTurn = false;　//自分のターン終わり
            isAttack = false;　//リセット
        }
    }
}
