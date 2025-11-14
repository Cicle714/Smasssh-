using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    float  MovePow;
    Vector3 Move;
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
        Move = transform.position - FindObjectOfType<Player>().transform.position;
        if (Move.magnitude > MovePow)
        {
            Move = Move.normalized * MovePow;
        }
        rb.linearVelocity = Move;
    }
}
