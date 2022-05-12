using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]

// �u���b�N�ђʃA�C�e���̃N���X
public class Item_penetrate : MonoBehaviour
{
    private bool isActivated = false; // �A�C�e���̎g�p���
    private int lifetime = 1000; // �A�C�e���̌��ʎ�������
    private AudioClip se_penetrate;

    // ���ʂ�K�p����
    private void Activate()
    {
        // �u���b�N�ɓ����������̔��˂𖳂���
        gameObject.GetComponent<BallController>().isReflect = false;

        isActivated = true;

        // �A�C�e��������摜���폜
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();

        // �A�C�e���u���b�N��L����
        GameObject.Find("Canvas").GetComponent<ItemController>().ActivateItemBlock();

        // SE��炷
        AudioSource.PlayClipAtPoint(se_penetrate, new Vector3(0.0f, 0.0f, -10.0f));

        // �{�[���̌����ڂ�ς���
        gameObject.GetComponent<BallController>().ChangeSpriteToPenetrateBall();
    }

    // ���ʂ��I�����Č��ɖ߂�
    private void Deactivate() {
        // �u���b�N�ɓ����������̔��˂��ĊJ����
        gameObject.GetComponent<BallController>().isReflect = true;

        // �{�[���̌����ڂ����ɖ߂�
        gameObject.GetComponent<BallController>().ChangeSpriteToNormalBall();

        // �R���|�[�l���g���폜����
        Destroy(gameObject.GetComponent<Item_penetrate>());
    }

    // Start is called before the first frame update
    void Start()
    {
        // SE�ǂݍ���
        se_penetrate = Resources.Load<AudioClip>("Audio/sound_boost");
    }

    // Update is called once per frame
    void Update()
    {
        // �X�y�[�X�L�[�Ŏg�p�J�n
        if (Input.GetKey(KeyCode.Space) && isActivated == false)
        {
            Activate();
        }

        // �g�p���ł���Ύc�莞�Ԃ����Z����
        if (isActivated)
        {
            lifetime -= 1;
        }

        // ���ʎ��Ԃ��؂ꂽ��~�߂�
        if (lifetime == 0)
        {
            Deactivate();
        }
    }
}
