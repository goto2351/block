using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]

// �u���b�N�ђʃA�C�e���̃N���X
public class Item_penetrate : MonoBehaviour
{
    private bool isActivated = false; // �A�C�e���̎g�p���
    private int lifetime = 1000; // �A�C�e���̌��ʎ�������

    // ���ʂ�K�p����
    private void Activate()
    {
        // �u���b�N�ɓ����������̔��˂𖳂���
        gameObject.GetComponent<BallController>().isReflect = false;

        isActivated = true;

        // �A�C�e��������摜���폜
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();
    }

    // ���ʂ��I�����Č��ɖ߂�
    private void Deactivate() {
        // �u���b�N�ɓ����������̔��˂��ĊJ����
        gameObject.GetComponent<BallController>().isReflect = true;

        // �R���|�[�l���g���폜����
        Destroy(gameObject.GetComponent<Item_penetrate>());
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
