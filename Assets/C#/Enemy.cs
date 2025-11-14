using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    float  MovePow;
    Vector3 Move;
    private bool isAttack = false;
    private float AttackDelay = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyMove()
    {
        AttackDelay -= Time.deltaTime;
        if (AttackDelay < 0 && !isAttack)
        {
            MovePow = Random.Range(300, 500);

            isAttack = true;
            Move = transform.position - FindObjectOfType<Player>().transform.position;
            
            Move = Move.normalized * -MovePow * 0.05f;
            
            rb.linearVelocity = Move;
        }
        if (rb.linearVelocity == Vector3.zero && isAttack) {
            FindObjectOfType<GameManager>().myTurn = true;
            AttackDelay = 2;
            isAttack = false;
        }
    }
}
