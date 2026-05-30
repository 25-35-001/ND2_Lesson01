using UnityEngine;

//-----敵キャラクター-----
/*
 * プレイヤーを追いかけてくるキャラクター
 * しつこい性格
 */
public class Enemy : MonoBehaviour
{
    public Vector3 moveVelosity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //移動させる
        Movement();
    }

    //-----移動メソッド-----
    /*
     * 引数　：なし
     * 戻り値：なし(void)
     */
    public void Movement()
    {
        //移動ベクトルをプレイヤーに向ける
        //プレイヤーの座標を取得する
        Vector3 playerPoint = GameObject.Find("Unitychan_Idle_0").transform.position;
        //Enemy(自分)の座標を取得する
        Vector3 enemyPoint = transform.position;

        //ターゲットとの角度（ラジアン）を求める
        float diffX = playerPoint.x - enemyPoint.x;
        float diffY = playerPoint.y - enemyPoint.y;
        float radian = Mathf.Atan2(diffY, diffX);
        //角度から向きベクトルを求める
        float dy = Mathf.Sin(radian);
        float dx = Mathf.Cos(radian);

        //向きベクトルを移動ベクトルに代入する
        moveVelosity.x = dx;
        moveVelosity.y = dy;

        //移動ベクトルを使って座標を加算する
        transform.Translate(moveVelosity * Time.deltaTime);
    }


    //----衝突判定イベントメソッド
    /*
     * 引数１：衝突したオブジェクトの情報(Collision2D型)
     * 戻り値：なし
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{collision.transform.name}に衝突しました。");
        //[Player]というタグをもったオブジェクトにだけ衝突する
        if( collision.transform.tag == "Player")
        {
            //衝突の処理！
            //衝突したらプレイヤーの見た目（Renderer）非表示にする
            collision.transform.GetComponent<SpriteRenderer>().enabled = false;
            //[Unitychan.cs]の動きを止める
            collision.transform.GetComponent<Unitychan>().enabled = false;
            //プレイヤーの武器も非表示にする
            collision.transform.GetComponent<Unitychan>().weapon.SetActive(false);
        }

        //[weapon]というタグをもったオブジェクトにだけ衝突する
        if (collision.transform.tag == "weapon")
        {
            Destroy(gameObject);
        }
    }
}
