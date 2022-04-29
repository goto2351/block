using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class ItemController : MonoBehaviour
{
    [SerializeField] private Sprite sprite_item_boost; //�����A�C�e���̉摜
    [SerializeField] private Sprite sprite_item_rotate; // TODO ��]�A�C�e���̉摜��ݒ肷��
    [SerializeField] private GameObject itemBlock;
    // �A�C�e���̉摜���������p
    [SerializeField] private SpriteRenderer UI_ItemBox;
    private bool isStart;

    // itemblock�p�̃^�C�}�[�ƃt���O
    //private int itemBlockTimer;
    private bool isExist_itemBlock;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        //itemBlockTimer = 0;
        isExist_itemBlock = false;
    }

    public void StartGame()
    {
        //GameObject.Find("itemBox").GetComponent<SpriteRenderer>().sprite = sprite_item_boost;
        //isExist_itemBlock = true;
        isStart = true;
    }

    public void DestroyItemBlock()
    {
        isExist_itemBlock = false;
    }

    //(��)�A�C�e�����ɃA�C�e���̉摜��\������
    // todo: �A�C�e���{�b�N�X�ɓ���������ballcontroller��ontriggerenter2D��nullreferenceexception�ɂȂ�
    public void SetItemImage_Boost()
    {
        UI_ItemBox.sprite = sprite_item_boost;
        // ��:ball�ɃA�C�e�����A�^�b�`
        //GameObject.Find("ball").AddComponent<Assets.Scripts.item.item_booster>()
        GameObject.Find("ball").AddComponent<Item_boost>();
    }

    // ��]�A�C�e���̉摜��\���A�A�^�b�`
    public void SetItemImage_Rotate()
    {
        UI_ItemBox.sprite = sprite_item_rotate;
        GameObject.Find("ball").AddComponent<Item_rotate>();
    }

    public void ClearItemImage()
    {
        UI_ItemBox.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        // ��莞�Ԃ��ƂɃ����_���ɃA�C�e���u���b�N���o��������
        if (Random.Range(0, 1000) == 1 && isStart == true && isExist_itemBlock == false)
        {
            Vector3 pos = new Vector3(Random.Range(-6f, 6f), -1f, 0);
            Instantiate(itemBlock, pos, Quaternion.identity);
            isExist_itemBlock = true;
        }
    }

}
