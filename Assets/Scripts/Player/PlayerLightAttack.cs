using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LightController lightController;
    [SerializeField] private BatteryController batteryController;
    public BatteryConfig config;
    [SerializeField] private LayerMask ghostLayer;

    void Update()
    {
        // 常にライトの範囲内にいる幽霊を「見える」状態にする
        IlluminateGhosts();

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            PerformFlashAttack();
        }
    }

    void IlluminateGhosts()
    {
        // mainLightの現在のlocalScale.xを「半径」の基準にする
        float currentRadius = lightController.mainLight.transform.localScale.x;
        
        // 周囲の幽霊を検知
        Collider2D[] ghosts = Physics2D.OverlapCircleAll(transform.position, currentRadius, ghostLayer);
        foreach (var g in ghosts)
        {
            EnemyChaser ghost = g.GetComponent<EnemyChaser>();
            if (ghost != null) ghost.isIlluminated = true;
        }
    }

    void PerformFlashAttack()
    {
        // バッテリーチェック
        if (batteryController.currentBattery < config.lightAttackCost) return;

        // バッテリー消費
        batteryController.currentBattery -= config.lightAttackCost; // BatteryControllerのメソッドがあればそれに置き換えてください

        // 現在のライトのサイズに合わせて攻撃判定を飛ばす
        float attackRadius = lightController.mainLight.transform.localScale.x;
        Collider2D[] hitGhosts = Physics2D.OverlapCircleAll(transform.position, attackRadius, ghostLayer);

        foreach (var g in hitGhosts)
        {
            // 幽霊側の OnHit() を呼ぶ（前述のコード参照）
            g.GetComponent<EnemyChaser>()?.OnHit();
        }
        
        // 演出：ここで一瞬だけ subLight をパッと明るくするなどの処理を入れるとカッコいいです
    }
}