using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool myTurn = true;
    [SerializeField]
    private Enemy[] Enemys;
    private int EnemyNum = 0;
    public int Point;

    void Start()
    {
    }


    void Update()
    {
        if (myTurn)
            FindObjectOfType<Player>().Move();　//自分のターンの行動
        else
            Enemys[EnemyNum].EnemyMove(); //敵のターンの行動
        if (Enemys[EnemyNum].isAttack || Enemys[EnemyNum].Death)
        {
            EnemyNum++;
            if (EnemyNum >= Enemys.Length)
            {
                for (int i = 0; i < Enemys.Length; i++)
                {
                    myTurn = true;
                    Enemys[i].isAttack = false;

                }
                EnemyNum = 0;
            }
        }

        if(Point >= Enemys.Length)
        {

        }

    }
}
