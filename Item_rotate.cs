using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]
public class Item_rotate : MonoBehaviour
{
    private bool isActivated = false; //�A�C�e���̎g�p���
    private AudioClip se_rotate;

    // �A�C�e���̌��ʂ𔭓�����
    private void Activate()
    {
        // �����_���Ȍ����̕����x�N�g���𐶐�����
        float rad = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 new_velocity = new Vector3(Mathf.Cos(rad),  Mathf.Sin(rad), 0);

        // �����x�N�g����ball�ɓK�p����
        gameObject.GetComponent<BallController>().velocity = new_velocity;

        // SE��炷
        AudioSource.PlayClipAtPoint(se_rotate, new Vector3(0.0f, 0.0f, -5.0f));

        // �A�C�e��������摜���폜
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();

        // �A�C�e���u���b�N��L����
        GameObject.Find("Canvas").GetComponent<ItemController>().ActivateItemBlock();
    }
    // Start is called before the first frame update
    void Start()
    {
        //se�ǂݍ���
        se_rotate = Resources.Load<AudioClip>("Audio/item_rotate");
    }

    // Update is called once per frame
    void Update()
    {
        // �X�y�[�X�L�[�Ŏg�p�J�n
        if (Input.GetKey(KeyCode.Space) && isActivated == false)
        {
            Activate();

            // ���ʂ𔭓�������R���|�[�l���g�����ł�����
            Destroy(gameObject.GetComponent<Item_rotate>());
        }

    }
}
