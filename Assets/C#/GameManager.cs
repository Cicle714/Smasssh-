using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool myTurn = true;
    [SerializeField]
    private Enemy[] Enemys;
    private int EnemyNum = 0;
    public int Point;
    [SerializeField]
    private GameObject ClearText;

    [SerializeField]
    private Image BlackOut;
    private float BlackOutCount;
    private float BlackOutTime = 2;

    void Start()
    {
        StartCoroutine(_BlackOut());
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
            ClearText.SetActive(true);
            BlackOutCount += Time.deltaTime;
            if(BlackOutCount >= BlackOutTime)
            {
                BlackOut.color += new Color(0, 0, 0, 1 * Time.deltaTime / 2);
                if(BlackOut.color.a >= 1)
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }

    }

    IEnumerator _BlackOut()
    {
        while(BlackOut.color.a > 0)
        {
            BlackOut.color -= new Color(0, 0, 0, 1 * Time.deltaTime / 2);
            yield return null;
        }

    }
}
