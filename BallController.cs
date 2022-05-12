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
    HashSet<string> hit_list = new HashSet<string>(); // ���ɓ��������u���b�N�̃��X�g

    //�{�[���̌�����
    [SerializeField] Sprite ball_normal;
    [SerializeField] Sprite ball_penetrate;

    private AudioClip[] se_hitBlock = new AudioClip[8]; // �u���b�N�ɓ����������̌��ʉ�
    private AudioClip se_itemBlock; // �A�C�e���u���b�N�ɓ����������̌��ʉ�

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);

        // SE�̓ǂݍ���
        SetSe_hitBlock();
        se_itemBlock = Resources.Load<AudioClip>("Audio/itemBlock");
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

    // �����ڂ�����{�[���ɂ�����(�A�C�e���p)
    public void ChangeSpriteToPenetrateBall()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ball_penetrate;
    }

    // �{�[���̌����ڂ�ʏ�ɂ���(�{�[���p)
    public void ChangeSpriteToNormalBall()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ball_normal;
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
            AudioSource.PlayClipAtPoint(se_itemBlock, new Vector3(0.0f, 0.0f, -10.0f)); // SE��炷
            // TODO �����̒��I�Ƃ��͕ʂ̏ꏊ�Ɉڂ���������������
            // �A�C�e���̒��I
            int item = Random.Range(0, 3);
            // �A�C�e���u���b�N�̖�����(->�A�C�e���g�p���ɗL��������)
            GameObject.Find("Canvas").GetComponent<ItemController>().DeactivateItemBlock();

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
            // ���ɓ��������u���b�N�̃��X�g�ɓ����Ă��Ȃ���ΏՓˏ��������s����
            if (hit_list.Add(collision.gameObject.transform.parent.name))
            {
                combo += 1;
                GameObject.Find("Canvas").GetComponent<UIController>().PrintCombo(combo);

                int point = Mathf.CeilToInt(100.0f * Mathf.Pow(1.5f, combo - 1.0f));
                GameObject.Find("Canvas").GetComponent<UIController>().AddScore(point); //�X�R�A���Z�̃T���v��

                // SE��炷
                int seNum = combo - 1;
                if (combo > 8) { seNum = 7; }
                AudioSource.PlayClipAtPoint(se_hitBlock[seNum], new Vector3(0.0f, 0.0f, -10.0f));

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

                    // �A�C�e���{�b�N�X������Ώ���
                    if (GameObject.FindGameObjectWithTag("itemBlock") != null)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("itemBlock"));
                    }

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


    // �u���b�N�ɓ����������̌��ʉ����Z�b�g����
    private void SetSe_hitBlock()
    {
        for (int i = 0; i < 8; i++)
        {
            int num = i + 1;
            se_hitBlock[i] = Resources.Load<AudioClip>("Audio/hitBlock_" + num.ToString());
        }
    }
}
