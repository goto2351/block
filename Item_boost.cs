using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����A�C�e���̃R���|�[�l���g
[RequireComponent(typeof(BallController))]
public class Item_boost : MonoBehaviour
{
    private float defaultSpeed; // ���ʓK�p�O�̃X�s�[�h
    private int lifetime = 500; // ���ʂ̎�������
    private bool isActivated = false; // �g�p���
    private�@AudioClip se_boost;

    // ���ʂ�K�p����
    private void Activate()
    {
        // �����O�̃X�s�[�h��ێ����Ă���
        defaultSpeed = gameObject.GetComponent<BallController>().speed;
        // ��������
        gameObject.GetComponent<BallController>().speed *= 1.5f;
        isActivated = true;

        // SE��炷
        AudioSource.PlayClipAtPoint(se_boost, new Vector3(0.0f, 0.0f, -10.0f));

        // �A�C�e��������摜���폜
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();
    }

    // ���ʂ��I������
    private void Deactivate()
    {
        // �{�[���̃X�s�[�h�������O�̂��̂ɖ߂�
        gameObject.GetComponent<BallController>().speed = defaultSpeed;

        // �R���|�[�l���g���폜����
        Destroy(gameObject.GetComponent<Item_boost>());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // SE�ǂݍ���
        se_boost = Resources.Load<AudioClip>("Audio/sound_boost");
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
