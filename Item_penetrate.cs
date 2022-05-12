using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]

// ブロック貫通アイテムのクラス
public class Item_penetrate : MonoBehaviour
{
    private bool isActivated = false; // アイテムの使用状態
    private int lifetime = 1000; // アイテムの効果持続時間
    private AudioClip se_penetrate;

    // 効果を適用する
    private void Activate()
    {
        // ブロックに当たった時の反射を無くす
        gameObject.GetComponent<BallController>().isReflect = false;

        isActivated = true;

        // アイテム欄から画像を削除
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();

        // アイテムブロックを有効化
        GameObject.Find("Canvas").GetComponent<ItemController>().ActivateItemBlock();

        // SEを鳴らす
        AudioSource.PlayClipAtPoint(se_penetrate, new Vector3(0.0f, 0.0f, -10.0f));

        // ボールの見た目を変える
        gameObject.GetComponent<BallController>().ChangeSpriteToPenetrateBall();
    }

    // 効果を終了して元に戻す
    private void Deactivate() {
        // ブロックに当たった時の反射を再開する
        gameObject.GetComponent<BallController>().isReflect = true;

        // ボールの見た目を元に戻す
        gameObject.GetComponent<BallController>().ChangeSpriteToNormalBall();

        // コンポーネントを削除する
        Destroy(gameObject.GetComponent<Item_penetrate>());
    }

    // Start is called before the first frame update
    void Start()
    {
        // SE読み込み
        se_penetrate = Resources.Load<AudioClip>("Audio/sound_boost");
    }

    // Update is called once per frame
    void Update()
    {
        // スペースキーで使用開始
        if (Input.GetKey(KeyCode.Space) && isActivated == false)
        {
            Activate();
        }

        // 使用中であれば残り時間を減算する
        if (isActivated)
        {
            lifetime -= 1;
        }

        // 効果時間が切れたら止める
        if (lifetime == 0)
        {
            Deactivate();
        }
    }
}
