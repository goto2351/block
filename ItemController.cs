using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class ItemController : MonoBehaviour
{
    [SerializeField] private Sprite sprite_item_boost; //加速アイテムの画像
    [SerializeField] private Sprite sprite_item_rotate; // 回転アイテムの画像
    [SerializeField] private Sprite sprite_item_penetrate; // 貫通アイテムの画像
    [SerializeField] private GameObject itemBlock;
    // アイテムの画像書き換え用
    [SerializeField] private SpriteRenderer UI_ItemBox;
    private bool isStart;

    // itemblock用のタイマーとフラグ
    //private int itemBlockTimer;
    private bool isActivate_itemBlock;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        //itemBlockTimer = 0;
        isActivate_itemBlock = true;
    }

    public void StartGame()
    {
        //GameObject.Find("itemBox").GetComponent<SpriteRenderer>().sprite = sprite_item_boost;
        //isActivate_itemBlock = true;
        isStart = true;
    }

    public void EndGame()
    {
        isStart = false;
    }

    public void DestroyItemBlock()
    {
        isActivate_itemBlock = true;
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

    // 回転アイテムの画像を表示、アタッチ
    public void SetItemImage_Rotate()
    {
        UI_ItemBox.sprite = sprite_item_rotate;
        GameObject.Find("ball").AddComponent<Item_rotate>();
    }

    // 貫通アイテムの画像を表示、アタッチ
    public void SetItemImage_Penetrate()
    {
        UI_ItemBox.sprite = sprite_item_penetrate;
        GameObject.Find("ball").AddComponent<Item_penetrate>();
    }

    public void ClearItemImage()
    {
        UI_ItemBox.sprite = null;
    }


    public void ActivateItemBlock()
    {
        isActivate_itemBlock = true;
    }

    public void DeactivateItemBlock()
    {
        isActivate_itemBlock = false;
    }
    // Update is called once per frame
    void Update()
    {
        // 一定時間ごとにランダムにアイテムブロックを出現させる
        if (Random.Range(0, 1000) == 1 && isStart == true && isActivate_itemBlock == true)
        {
            Vector3 pos = new Vector3(Random.Range(-6f, 6f), -1f, 0);
            Instantiate(itemBlock, pos, Quaternion.identity);
            isActivate_itemBlock = false;
        }
    }

}
