using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]

public class BallController : MonoBehaviour
{
    public Vector3 velocity;
    private int combo;
    private int maxCombo = 0; // 最大コンボ
    public float speed;
    bool isStart;
    public bool isReflect = true; // ブロックと当たった時に反射するかどうか
    HashSet<string> hit_list = new HashSet<string>(); // 既に当たったブロックのリスト

    //ボールの見た目
    [SerializeField] Sprite ball_normal;
    [SerializeField] Sprite ball_penetrate;

    private AudioClip[] se_hitBlock = new AudioClip[8]; // ブロックに当たった時の効果音
    private AudioClip se_itemBlock; // アイテムブロックに当たった時の効果音

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);

        // SEの読み込み
        SetSe_hitBlock();
        se_itemBlock = Resources.Load<AudioClip>("Audio/itemBlock");
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

    // 見た目を光るボールにかえる(アイテム用)
    public void ChangeSpriteToPenetrateBall()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ball_penetrate;
    }

    // ボールの見た目を通常にする(ボール用)
    public void ChangeSpriteToNormalBall()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ball_normal;
    }

    // オブジェクトと衝突したときの処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // パドルか上の壁に当たった時
        if (collision.gameObject.tag == "paddle")
        {
            // 最大コンボの更新
            if (maxCombo < combo)
            {
                maxCombo = combo;
            }

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
            // 最大コンボの更新
            if (maxCombo < combo)
            {
                maxCombo = combo;
            }

            GameObject.Find("Canvas").GetComponent<UIController>().PrintMessage_GameOver(maxCombo);

            // アイテムボックスが追加されないようにする
            GameObject.Find("Canvas").GetComponent<ItemController>().EndGame();

            // ボールを消滅させる
            Destroy(gameObject);

        } else if (collision.gameObject.tag == "itemBlock")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Canvas").GetComponent<ItemController>().DestroyItemBlock();
            AudioSource.PlayClipAtPoint(se_itemBlock, new Vector3(0.0f, 0.0f, -10.0f)); // SEを鳴らす
            // TODO ここの抽選とかは別の場所に移した方がいいかも
            // アイテムの抽選
            int item = Random.Range(0, 3);
            // アイテムブロックの無効化(->アイテム使用時に有効化する)
            GameObject.Find("Canvas").GetComponent<ItemController>().DeactivateItemBlock();

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

                case 2:
                    // 貫通アイテム
                    GameObject.Find("Canvas").GetComponent<ItemController>().SetItemImage_Penetrate();
                    break;
            }

        }
        else if (collision.gameObject.transform.parent.tag == "block") //ブロックに当たった時(当たり判定のオブジェクトが子なので親で判定)
        {
            // 既に当たったブロックのリストに入っていなければ衝突処理を実行する
            if (hit_list.Add(collision.gameObject.transform.parent.name))
            {
                combo += 1;
                GameObject.Find("Canvas").GetComponent<UIController>().PrintCombo(combo);

                int point = Mathf.CeilToInt(100.0f * Mathf.Pow(1.5f, combo - 1.0f));
                GameObject.Find("Canvas").GetComponent<UIController>().AddScore(point); //スコア加算のサンプル

                // SEを鳴らす
                int seNum = combo - 1;
                if (combo > 8) { seNum = 7; }
                AudioSource.PlayClipAtPoint(se_hitBlock[seNum], new Vector3(0.0f, 0.0f, -10.0f));

                Destroy(collision.gameObject.transform.parent.gameObject);

                // 残りブロックが0個になったらゲームクリア
                if (GameObject.FindGameObjectsWithTag("block").Length == 1)
                {
                    // 最大コンボの更新
                    if (maxCombo < combo)
                    {
                        maxCombo = combo;
                    }

                    // メッセージの表示
                    GameObject.Find("Canvas").GetComponent<UIController>().PrintMessage_GameClear(maxCombo);

                    // アイテムボックスがあれば消す
                    if (GameObject.FindGameObjectWithTag("itemBlock") != null)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("itemBlock"));
                    }

                    // アイテムボックスが追加されないようにする
                    GameObject.Find("Canvas").GetComponent<ItemController>().EndGame();

                    // ボールを消滅させる
                    Destroy(gameObject);
                }

                // ボールの反射
                if (isReflect == true)
                {
                    if (collision.gameObject.tag == "blockCol_top") //ブロックの上下に当たった時
                    {
                        //todo; 角に当たった時スコアの加算が二回呼ばれる
                        // ブロックに当たったら跳ね返らせてブロックを消す
                        // todo: スコア、コンボの加算
                        velocity.y *= -1.0f;
                    }
                    else //ブロックの左右に当たった時
                    {
                        velocity.x *= -1.0f;
                    }
                }
            }
        }
    }


    // ブロックに当たった時の効果音をセットする
    private void SetSe_hitBlock()
    {
        for (int i = 0; i < 8; i++)
        {
            int num = i + 1;
            se_hitBlock[i] = Resources.Load<AudioClip>("Audio/hitBlock_" + num.ToString());
        }
    }
}
