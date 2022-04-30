using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]

public class BallController : MonoBehaviour
{
    public Vector3 velocity;
    private int combo;
    private int maxCombo = 0; // �ő�R���{
    public float speed;
    bool isStart;
    public bool isReflect = true; // �u���b�N�Ɠ����������ɔ��˂��邩�ǂ���

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += velocity * speed * Time.deltaTime;

        // �E�N���b�N�ŃA�C�e���g�p
    }

    // UIController����Ăяo���ăp�h���̏����ړ����s��
    public void StartGame()
    {
        //�����_���ɏ����̈ړ����������߂�
        float rad = Random.Range(30.0f, 150.0f) * Mathf.Deg2Rad;
        velocity = new Vector3(Mathf.Cos(rad), -1.0f * Mathf.Sin(rad), 0);
    }



    // �I�u�W�F�N�g�ƏՓ˂����Ƃ��̏���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �p�h������̕ǂɓ���������
        if (collision.gameObject.tag == "paddle")
        {
            // �ő�R���{�̍X�V
            if (maxCombo < combo)
            {
                maxCombo = combo;
            }

            combo = 0;
            GameObject.Find("Canvas").GetComponent<UIController>().PrintCombo(combo);

            velocity.y *= -1.0f;
        } else if (collision.gameObject.tag == "wall_top")
        {
            velocity.y *= -1.0f;
        }
        else if (collision.gameObject.tag == "wall_side")
        {
            // ���̕ǂɓ���������
            velocity.x *= -1.0f;
        } else if (collision.gameObject.tag == "wall_bottom")
        {
            // �ő�R���{�̍X�V
            if (maxCombo < combo)
            {
                maxCombo = combo;
            }

            GameObject.Find("Canvas").GetComponent<UIController>().PrintMessage_GameOver(maxCombo);

            // �A�C�e���{�b�N�X���ǉ�����Ȃ��悤�ɂ���
            GameObject.Find("Canvas").GetComponent<ItemController>().EndGame();

            // �{�[�������ł�����
            Destroy(gameObject);

        } else if (collision.gameObject.tag == "itemBlock")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Canvas").GetComponent<ItemController>().DestroyItemBlock();
            // TODO �����̒��I�Ƃ��͕ʂ̏ꏊ�Ɉڂ���������������
            // �A�C�e���̒��I
            int item = Random.Range(0, 3);
            // �I�΂ꂽ�A�C�e���ɉ����ĉ摜�̕\���A�R���|�[�l���g�̃A�^�b�`���s��
            switch (item)
            {
                case 0:
                    // �����A�C�e��
                    GameObject.Find("Canvas").GetComponent<ItemController>().SetItemImage_Boost();
                    break;

                case 1:
                    // ��]�A�C�e��
                    GameObject.Find("Canvas").GetComponent<ItemController>().SetItemImage_Rotate();
                    break;

                case 2:
                    // �ђʃA�C�e��
                    GameObject.Find("Canvas").GetComponent<ItemController>().SetItemImage_Penetrate();
                    break;
            }

        }
        else if (collision.gameObject.transform.parent.tag == "block") //�u���b�N�ɓ���������(�����蔻��̃I�u�W�F�N�g���q�Ȃ̂Őe�Ŕ���)
        {
            combo += 1;
            GameObject.Find("Canvas").GetComponent<UIController>().PrintCombo(combo);

            int point = Mathf.CeilToInt(100.0f * Mathf.Pow(1.5f, combo - 1.0f));
            GameObject.Find("Canvas").GetComponent<UIController>().AddScore(point); //�X�R�A���Z�̃T���v��

            Destroy(collision.gameObject.transform.parent.gameObject);

            // �c��u���b�N��0�ɂȂ�����Q�[���N���A
            if (GameObject.FindGameObjectsWithTag("block").Length == 1)
            {
                // �ő�R���{�̍X�V
                if (maxCombo < combo)
                {
                    maxCombo = combo;
                }

                // ���b�Z�[�W�̕\��
                GameObject.Find("Canvas").GetComponent<UIController>().PrintMessage_GameClear(maxCombo);

                // �A�C�e���{�b�N�X���ǉ�����Ȃ��悤�ɂ���
                GameObject.Find("Canvas").GetComponent<ItemController>().EndGame();

                // �{�[�������ł�����
                Destroy(gameObject);
            }

            // �{�[���̔���
            if (isReflect == true)
            {
                if (collision.gameObject.tag == "blockCol_top") //�u���b�N�̏㉺�ɓ���������
                {
                    //todo; �p�ɓ����������X�R�A�̉��Z�����Ă΂��
                    // �u���b�N�ɓ��������璵�˕Ԃ点�ău���b�N������
                    // todo: �X�R�A�A�R���{�̉��Z
                    velocity.y *= -1.0f;
                }
                else //�u���b�N�̍��E�ɓ���������
                {
                    velocity.x *= -1.0f;
                }
            }
        }
       
        
    }
}
