using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class ItemController : MonoBehaviour
{
    [SerializeField] private Sprite sprite_item_boost;
    [SerializeField] private GameObject itemBlock;
    // アイテムの画像書き換え用
    [SerializeField] private SpriteRenderer UI_ItemBox;
    private bool isStart;

    // itemblock用のタイマーとフラグ
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

    //(仮)アイテム欄にアイテムの画像を表示する
    // todo: アイテムボックスに当たった時ballcontrollerのontriggerenter2Dでnullreferenceexceptionになる
    public void SetItemImage_Boost()
    {
        UI_ItemBox.sprite = sprite_item_boost;
        // 仮:ballにアイテムをアタッチ
        //GameObject.Find("ball").AddComponent<Assets.Scripts.item.item_booster>()
        GameObject.Find("ball").AddComponent<Item_boost>();
    }

    public void ClearItemImage()
    {
        UI_ItemBox.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        // 一定時間ごとにランダムにアイテムブロックを出現させる
        if (Random.Range(0, 1000) == 1 && isStart == true && isExist_itemBlock == false)
        {
            Vector3 pos = new Vector3(Random.Range(-6f, 6f), -1f, 0);
            Instantiate(itemBlock, pos, Quaternion.identity);
            isExist_itemBlock = true;
        }
    }

}
