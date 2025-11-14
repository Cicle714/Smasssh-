using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public bool myTurn = true;

    void Start()
    {
        
    }


    void Update()
    {
        if (myTurn)
            FindObjectOfType<Player>().Move();
        else
            FindObjectOfType<Enemy>().EnemyMove();
    }
}
