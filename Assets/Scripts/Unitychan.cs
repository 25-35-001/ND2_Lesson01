using UnityEngine;
using UnityEngine.InputSystem;

public class Unitychan : MonoBehaviour
{
    public Vector2 Velocity;
    public Animator Animator;
    public SpriteRenderer Renderer;

    public bool isWalk = false;
    public Vector2 mousePoint;

    public Vector3 targetPoint;
    public GameObject weapon;

    public ParticleSystem footStampEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーションを動かす
        Animator.SetFloat("Velocity", Mathf.Abs(Velocity.x)*(isWalk ? 0.5f : 1));
        //スプライトの反転
        if(Velocity.x <= -0.1f)Renderer.flipX = true;
        else if(0.1f <= Velocity.x)Renderer.flipX = false;
        //座標を移動させる
        float speed = Velocity.x * 5 * (isWalk ? 0.5f : 1) * Time.deltaTime;
        transform.Translate(speed, 0, 0);

        //ターゲット（マウス）の位置を保存し続ける
        targetPoint = GetMousePoint();
        //ターゲットとの角度（ラジアン）を求める
        float diffX = targetPoint.x - transform.position.x;
        float diffY = targetPoint.y - transform.position.y;
        float radian = Mathf.Atan2(diffY,diffX);
        //角度から向きベクトルを求める
        float dy = Mathf.Sin(radian);
        float dx = Mathf.Cos(radian);

        weapon.transform.eulerAngles = new Vector3(0, 0, radian * 180 / Mathf.PI);

        FootStampControl();
    }

    void OnMove(InputValue value)
    {
        Velocity = value.Get<Vector2>();
    }

    void OnClick(InputValue value)
    {
        isWalk = value.Get<float>() >= 0.5f;
    }

    //マウスの座標の入力
    void OnMousePoint(InputValue value)
    {
        mousePoint = value.Get<Vector2>();
    }
    //マウス座標を取得するメソッド
    //引　数；なし
    //戻り値；<Vector2型>の値
    Vector2 GetMousePoint()
    { 
        //マウスの座標を取得
        Vector2 mousePoint = this.mousePoint;

        //メインカメラの情報を取得
        Camera camera = Camera.main;

        //[px]単位を[m]単位に変換
        Vector3 worldPoint = camera.ScreenToWorldPoint(mousePoint);

        //変換した値を返す
        return worldPoint;
    }

    //---足跡パーティクルの制御
    //引数　：なし
    //戻り値：なし
    public void FootStampControl()
    { 
        //->三項演算子[条件式？　true : false;]
        //1行で書けるif文...でも1行しか書けない...
        //制御が1行でできる単純な場合に使用する
        bool isStamp = Velocity.magnitude > 0.1f ? true : false;

        if (isStamp && !footStampEffect.isPlaying)
        {//動いていれば、エフェクトを出す
            footStampEffect.Play();
        }
        if(!isStamp)
        {//動いていない時は、エフェクトを止める
            footStampEffect.Stop();
        }
    }

}
