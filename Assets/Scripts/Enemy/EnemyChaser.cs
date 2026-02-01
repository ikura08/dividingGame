using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 0.03f;
    [SerializeField] private float chaseInterval = 0.05f;

    private SpriteRenderer sr;
    public bool isIlluminated = false; // PlayerAttackから毎フレーム書き換えられる
    private bool isMoving = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        // 最初は姿を隠しておく
        sr.enabled = false;
    }

    void Update()
    {
        // 1. 光に当たっている時だけ描画する
        sr.enabled = isIlluminated;

        // 2. 移動処理
        if (!isMoving)
        {
            StartCoroutine(Chase());
        }

        // 光の判定はPlayer側で毎フレーム更新されるので、ここでリセットしておく
        isIlluminated = false;
    }

    IEnumerator Chase()
    {
        isMoving = true;

        if (player != null)
        {
            // プレイヤーの方向を計算して少し動く
            Vector3 direction = player.transform.position - transform.position;
            Vector3 unitVector = direction.normalized;
            transform.position += unitVector * moveSpeed;
        }

        yield return new WaitForSeconds(chaseInterval);
        isMoving = false;
    }

    // PlayerAttackスクリプトから、Shift攻撃が当たった時に呼ばれるメソッド
    public void OnHit()
    {
        // 姿が見えている（光に当たっている）時だけ倒せる
        if (sr.enabled)
        {
            Debug.Log("幽霊を撃破！");
            // ここで倒した時のエフェクト生成やSE再生を入れる
            Destroy(gameObject);
        }
    }
}