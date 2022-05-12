using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject comboText;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject endText;
    [SerializeField] GameObject UICanvas; //ゲーム開始時にitemcontrollerをアタッチする
    int score;
    bool isStart;
    bool isEnd = false; // ゲーム終了フラグ

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<Text>().text = "Score: " + score.ToString("D6");
        if (Input.GetMouseButtonUp(0) && isStart == false && isEnd == false)
        {
            Destroy(startText);
            GameObject.Find("ball").GetComponent<BallController>().StartGame();
            GameObject.Find("Canvas").GetComponent<ItemController>().StartGame();
            isStart = true;
        }
    }

    // 得点を加算するメソッド
    // 引数: (コンボなどを加味した後の)加算する得点
    public void AddScore(int point)
    {
        score += point;
    }

    public void PrintCombo(int combo)
    {
        if (combo > 0)
        {
            comboText.GetComponent<Text>().text = combo.ToString() + " combo";
        }else if (combo == 0)
        {
            comboText.GetComponent<Text>().text = "";
        }
    }

    // ゲームオーバー時のテキストを表示する
    public void PrintMessage_GameOver(int maxCombo)
    {
        endText.GetComponent<Text>().text = "Game Over\n" +
            "score: " + score.ToString("D6") + "   max combo: " + maxCombo.ToString("D3");

        isEnd = true;
    }

    // ゲームクリア時のテキストを表示する
    public void PrintMessage_GameClear(int maxCombo)
    {
        endText.GetComponent<Text>().text = "Stage Clear!\n" +
            "score: " + score.ToString("D6") + "   max combo: " + maxCombo.ToString("D3");

        isEnd = true;
    }
}
