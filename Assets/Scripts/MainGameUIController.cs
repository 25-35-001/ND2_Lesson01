using UnityEngine;
using UnityEngine.UIElements;


//==
//インゲーム(MainGame)のUIを制御するスクリプト
//==
public class MainGameUIController : MonoBehaviour
{
    //---変数宣言---
    public UIDocument gameUI;//UIDocument　コンポーネント

    private VisualElement _rootElement;//UIのroot(根本)のエレメント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rootElement = gameUI.rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {
        //時間の表示を更新
        TimerViewUpdate(Time.time);
    }

    //----
    //時間表示を更新するメソッド
    //・引数　：時間(float)
    //・戻り値：なし
    //----
    public void TimerViewUpdate(float viewTime)
    {
        //時間表示のラベルを取得
        //->VisualElement.Q<T>(エレメント名)※<T>にはエレメントの種類
        Label view = _rootElement.Q<Label>("timer-text");

        //ラベルのテキストを書き換える
        view.text = viewTime.ToString("f2");
    }
}
