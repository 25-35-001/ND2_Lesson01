using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("生成の設定")]
    [SerializeField] private GameObject enemyPrefab;    // 敵のプレハブ
    [SerializeField] private float spawnInterval = 2.0f; // 生成の間隔（秒）

    [Header("ランダム範囲の設定")]
    [SerializeField] private float minX = -8.0f;         // X座標の最小値
    [SerializeField] private float maxX = 8.0f;          // X座標の最大値

    private float timer; // 時間計測用

    void Update()
    {
        // 1. 毎フレームの経過時間を加算
        timer += Time.deltaTime;

        // 2. 設定した間隔を超えたかチェック
        if (timer >= spawnInterval)
        {
            SpawnEnemy(); // 生成メソッドの実行
            timer = 0;    // タイマーをリセット
        }
    }

    /// <summary>
    /// 指定された範囲内のランダムな位置に敵を生成する
    /// </summary>
    void SpawnEnemy()
    {
        // ランダムなX座標を計算
        float randomX = Random.Range(minX, maxX);

        // 生成する位置を決定（YとZはSpawner自身の位置を使用）
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // 敵を生成
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}