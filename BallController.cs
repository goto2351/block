using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]

public class BallController : MonoBehaviour
{
    public Vector3 velocity;
    private int combo;
    public float speed;
    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += velocity * speed * Time.deltaTime;

        // 右クリックでアイテム使用
    }

    // UIControllerから呼び出してパドルの初期移動を行う
    public void StartGame()
    {
        //ランダムに初期の移動方向を決める
        float rad = Random.Range(30.0f, 150.0f) * Mathf.Deg2Rad;
        velocity = new Vector3(Mathf.Cos(rad), -1.0f * Mathf.Sin(rad), 0);
    }



    // オブジェクトと衝突したときの処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // パドルか上の壁に当たった時
        if (collision.gameObject.tag == "paddle")
        {
            combo = 0;
            GameObject.Find("Canvas").GetComponent<UIController>().PrintCombo(combo);

            velocity.y *= -1.0f;
        } else if (collision.gameObject.tag == "wall_top")
        {
            velocity.y *= -1.0f;
        } 
        else if (collision.gameObject.tag == "wall_side")
        {
            // 横の壁に当たった時
            velocity.x *= -1.0f;
        } else if (collision.gameObject.tag == "wall_bottom")
        {
            // todo: 下の壁に当たった時にゲームオーバー
        } else if (collision.gameObject.tag == "itemBlock")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Canvas").GetComponent<ItemController>().DestroyItemBlock();
            // TODO ここの抽選とかは別の場所に移した方がいいかも
            // アイテムの抽選
            int item = Random.Range(0, 2);
            // 選ばれたアイテムに応じて画像の表示、コンポーネントのアタッチを行う
            switch (item)
            {
                case 0:
                    // 加速アイテム
                    GameObject.Find("Canvas").GetComponent<ItemController>().SetItemImage_Boost();
                    break;

                case 1:
                    // 回転アイテム
                    GameObject.Find("Canvas").GetComponent<ItemController>().SetItemImage_Rotate();
                    break;
            }
            
        }
        else if (collision.gameObject.transform.parent.tag == "block") //ブロックに当たった時(当たり判定のオブジェクトが子なので親で判定)
        {
            combo += 1;
            GameObject.Find("Canvas").GetComponent<UIController>().PrintCombo(combo);

            int point = Mathf.CeilToInt(100.0f * Mathf.Pow(1.5f, combo - 1.0f));
            GameObject.Find("Canvas").GetComponent<UIController>().AddScore(point); //スコア加算のサンプル

            Destroy(collision.gameObject.transform.parent.gameObject);

            if (collision.gameObject.tag == "blockCol_top") //ブロックの上下に当たった時
            {
                //todo; 角に当たった時スコアの加算が二回呼ばれる
                // ブロックに当たったら跳ね返らせてブロックを消す
                // todo: スコア、コンボの加算
                velocity.y *= -1.0f;
            } else //ブロックの左右に当たった時
            {
                velocity.x *= -1.0f;
            }
        }
       
        
    }
}
