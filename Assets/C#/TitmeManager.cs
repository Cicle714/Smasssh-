using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitmeManager : MonoBehaviour
{

    public int Level; //選択したレベル
    private bool OnPush;　//レベルを選択したか

    [SerializeField]
    private Image BlackImage;　//暗転用
    [SerializeField]
    private GameObject LevelSelect;　//Level Selectの表示
    private float SelectCount;　//点滅のカウント

    private float BlackOutCount = 1;　//暗転までのカウント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);　//シーン移動で消えないようにする
        StartCoroutine(BlackOut());　//最初に明るくする処理
    }

    // Update is called once per frame
    void Update()
    {
        if (OnPush)
        {
            SelectCount -= Time.deltaTime * 4;　//レベル選択で点滅を速くする
            BlackOutCount -= Time.deltaTime;　//暗転までのカウント
            if (BlackOutCount < 0)
            {
                if (BlackImage.color.a < 1)
                {
                    BlackImage.color += new Color(0, 0, 0, 1 * Time.deltaTime);　//暗転
                }
                else
                    SceneManager.LoadScene("PlayScene");　//シーン移動
            }
        }
        else
            SelectCount -= Time.deltaTime;　//点滅のカウントダウン

        //一秒ごとに点滅
        if (SelectCount < 0)
        {
            SelectCount = 1;
            if(LevelSelect.activeSelf)
                LevelSelect.SetActive(false);　
            else 
                LevelSelect.SetActive(true);
        }

    }

    /// <summary>
    /// Easyを選んだら
    /// </summary>
    public void OnEasy()
    {
        Level = 0;
        OnPush = true;
    }
    /// <summary>
    /// Normalを選んだら
    /// </summary>
    public void OnNormal()
    {
        Level = 1;
        OnPush = true;
    }
    /// <summary>
    /// Hardを選んだら
    /// </summary>
    public void OnHard()
    {
        Level = 2;
        OnPush = true;
    }

    /// <summary>
    /// 暗転処理
    /// </summary>
    /// <returns></returns>
    IEnumerator BlackOut()
    {
        while(BlackImage.color.a > 0 && !OnPush)
        {
            BlackImage.color -= new Color(0,0,0,1 * Time.deltaTime);
            yield return null;
        }
    }

}
