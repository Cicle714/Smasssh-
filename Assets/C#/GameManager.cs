using System.Collections;
using System.Collections.Generic;
using NUnit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player player;　

    public bool myTurn = true;　//自分のターン
    [SerializeField]
    private List<Enemy> Enemys;　//敵たち
    private int EnemyNum = 0;　//敵の番号
    public int Point;　//敵を倒した数
    [SerializeField]
    private GameObject ClearText; //クリア表示
    [SerializeField]
    private GameObject GameOverText;　//ゲームオーバー表示

    [SerializeField]
    private Image BlackOut;　//暗転用
    private float BlackOutCount; //暗転のカウント
    private float BlackOutTime = 2;　//暗転までの時間

    private int level;

    void Start()
    {
        player = FindObjectOfType<Player>();

        StartCoroutine(_BlackOut());　//暗転
        level = FindObjectOfType<TitmeManager>().Level;　//選択したレベルの取得
        Destroy(FindObjectOfType<TitmeManager>().gameObject);

        //選択レベルで敵の数を変える
        switch (level)
        {
            case 0:
                for(int i = Enemys.Count - 1; i > 0; i--)
                {
                    GameObject Ene = Enemys[i].gameObject;
                    Enemys.Remove(Enemys[i]);
                    Destroy(Ene);
                }
                break;
            case 1:
                for (int i = Enemys.Count - 2; i > 0; i--)
                {
                    GameObject Ene = Enemys[i].gameObject;
                    Enemys.Remove(Enemys[i]);
                    Destroy(Ene);
                }
                break;
        }
        
    }


    void Update()
    {

        //プレイヤーが落ちたらゲームオーバー
        if(player.transform.position.y < -1)
        {
            GameOverText.SetActive(true);
            BlackOutCount += Time.deltaTime;
            if (BlackOutCount >= BlackOutTime)
            {
                BlackOut.color += new Color(0, 0, 0, 1 * Time.deltaTime / 2);
                if (BlackOut.color.a >= 1)
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }
        

        if (myTurn)
            player.Move();　//自分のターンの行動
        else
            Enemys[EnemyNum].EnemyMove(); //敵のターンの行動

        //敵の行動順替えと死亡確認
        if (Enemys[EnemyNum].isAttack || Enemys[EnemyNum].Death)
        {
            EnemyNum++;
            if (EnemyNum >= Enemys.Count)
            {
                for (int i = 0; i < Enemys.Count; i++)
                {
                    myTurn = true;
                    Enemys[i].isAttack = false;

                }
                EnemyNum = 0;
            }
        }
        
        //敵が全滅したらタイトルに戻る
        if(Point >= Enemys.Count)
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

    /// <summary>
    /// 暗転処理
    /// </summary>
    /// <returns></returns>
    IEnumerator _BlackOut()
    {
        while(BlackOut.color.a > 0)
        {
            BlackOut.color -= new Color(0, 0, 0, 1 * Time.deltaTime / 2);
            yield return null;
        }

    }
}
