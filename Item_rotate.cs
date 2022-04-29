using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]
public class Item_rotate : MonoBehaviour
{
    private bool isActivated = false; //アイテムの使用状態

    // アイテムの効果を発動する
    private void Activate()
    {
        // ランダムな向きの方向ベクトルを生成する
        float rad = Random.Range(30.0f, 150.0f) * Mathf.Deg2Rad;
        Vector3 new_velocity = new Vector3(Mathf.Cos(rad), -1.0f * Mathf.Sin(rad), 0);

        // 方向ベクトルをballに適用する
        gameObject.GetComponent<BallController>().velocity = new_velocity;

        // アイテム欄から画像を削除
        GameObject.Find("Canvas").GetComponent<ItemController>().ClearItemImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
