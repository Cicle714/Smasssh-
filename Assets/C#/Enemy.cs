using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    float  MovePow;　//動きの強さ
    Vector3 Move;　//ベクトル
    public bool isAttack = false;　//攻撃状態
    [SerializeField]
    private float AttackDelay = 1.5f;　//攻撃までの時間
    private float DelayTime = 1.5f;
    public bool Death = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1f)
        {
            if (!Death)
            {
                FindObjectOfType<GameManager>().Point++;
                Death = true;
            }
        }
    }
    public void EnemyMove()
    {
        AttackDelay -= Time.deltaTime;　//攻撃のカウントダウン
        if (AttackDelay < 0 && !isAttack)
        {
            MovePow = Random.Range(300, 500);　//動きの強さをランダムに

            isAttack = true;　//攻撃した
            Move = transform.position - FindObjectOfType<Player>().transform.position;　//プレイヤーにベクトルを向ける
            
            Move = Move.normalized * -MovePow * 0.05f;　//動きの強さ調整
            
            rb.linearVelocity = Move;　//ベクトルを与える
            rb.AddTorque(Vector3.up * Random.Range(-500,500));
            AttackDelay = DelayTime;　//カウントダウンのリセット
        }
    }
}
