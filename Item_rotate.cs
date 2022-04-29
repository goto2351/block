using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]
public class Item_rotate : MonoBehaviour
{
    private bool isActivated = false; //�A�C�e���̎g�p���

    // �A�C�e���̌��ʂ𔭓�����
    private void Activate()
    {
        // �����_���Ȍ����̕����x�N�g���𐶐�����
        float rad = Random.Range(30.0f, 150.0f) * Mathf.Deg2Rad;
        Vector3 new_velocity = new Vector3(Mathf.Cos(rad), -1.0f * Mathf.Sin(rad), 0);

        // �����x�N�g����ball�ɓK�p����
        gameObject.GetComponent<BallController>().velocity = new_velocity;

        // �A�C�e��������摜���폜
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();
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

            // ���ʂ𔭓�������R���|�[�l���g�����ł�����
            Destroy(gameObject.GetComponent<Item_rotate>());
        }

    }
}