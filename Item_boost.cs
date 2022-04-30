using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 加速アイテムのコンポーネント
[RequireComponent(typeof(BallController))]
public class Item_boost : MonoBehaviour
{
    private float defaultSpeed; // 効果適用前のスピード
    private int lifetime = 500; // 効果の持続時間
    private bool isActivated = false; // 使用状態
    private　AudioClip se_boost;

    // 効果を適用する
    private void Activate()
    {
        // 加速前のスピードを保持しておく
        defaultSpeed = gameObject.GetComponent<BallController>().speed;
        // 加速する
        gameObject.GetComponent<BallController>().speed *= 1.5f;
        isActivated = true;

        // SEを鳴らす
        AudioSource.PlayClipAtPoint(se_boost, new Vector3(0.0f, 0.0f, -10.0f));

        // アイテム欄から画像を削除
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();
    }

    // 効果を終了する
    private void Deactivate()
    {
        // ボールのスピードを加速前のものに戻す
        gameObject.GetComponent<BallController>().speed = defaultSpeed;

        // コンポーネントを削除する
        Destroy(gameObject.GetComponent<Item_boost>());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // SE読み込み
        se_boost = Resources.Load<AudioClip>("Audio/sound_boost");
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
