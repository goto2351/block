using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]
public class Item_rotate : MonoBehaviour
{
    private bool isActivated = false; //アイテムの使用状態
    private AudioClip se_rotate;

    // アイテムの効果を発動する
    private void Activate()
    {
        // ランダムな向きの方向ベクトルを生成する
        float rad = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 new_velocity = new Vector3(Mathf.Cos(rad),  Mathf.Sin(rad), 0);

        // 方向ベクトルをballに適用する
        gameObject.GetComponent<BallController>().velocity = new_velocity;

        // SEを鳴らす
        AudioSource.PlayClipAtPoint(se_rotate, new Vector3(0.0f, 0.0f, -5.0f));

        // アイテム欄から画像を削除
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();

        // アイテムブロックを有効化
        GameObject.Find("Canvas").GetComponent<ItemController>().ActivateItemBlock();
    }
    // Start is called before the first frame update
    void Start()
    {
        //se読み込み
        se_rotate = Resources.Load<AudioClip>("Audio/item_rotate");
    }

    // Update is called once per frame
    void Update()
    {
        // スペースキーで使用開始
        if (Input.GetKey(KeyCode.Space) && isActivated == false)
        {
            Activate();

            // 効果を発動したらコンポーネントを消滅させる
            Destroy(gameObject.GetComponent<Item_rotate>());
        }

    }
}
