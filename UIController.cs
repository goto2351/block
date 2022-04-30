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
    [SerializeField] GameObject UICanvas; //�Q�[���J�n����itemcontroller���A�^�b�`����
    int score;
    bool isStart;

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
        if (Input.GetMouseButtonUp(0) && isStart == false)
        {
            Destroy(startText);
            GameObject.Find("ball").GetComponent<BallController>().StartGame();
            GameObject.Find("Canvas").GetComponent<ItemController>().StartGame();
        }
    }

    // ���_�����Z���郁�\�b�h
    // ����: (�R���{�Ȃǂ������������)���Z���链�_
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

    // �Q�[���I�[�o�[���̃e�L�X�g��\������
    public void PrintMessage_GameOver(int maxCombo)
    {
        endText.GetComponent<Text>().text = "Game Over\n" +
            "score: " + score.ToString("D6") + "   max combo: " + maxCombo.ToString("D3");
    }
}
