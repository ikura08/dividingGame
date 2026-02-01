using UnityEngine;

[CreateAssetMenu(fileName = "BatteryConfig", menuName = "Configs/BatteryConfig")]
public class BatteryConfig : ScriptableObject
{
    [Header("電池回復量")]
    public int batteryPickupRecover = 35;

    [Header("敵撃破での回復量")]
    public int enemybatteryRecover = 35;

    [Header("毎秒のの消費量")]
    public int seondCost = 3;

    [Header("弾の消費量")]
    public int bulletCost = 5;

    [Header("コアの消費量")]
    public int coreBatteryCost = 20;

    [Header("敵に当たった時の消費量")]
    public int enemyContactDamage = 7;

    [Header("敵の弾に当たった時の消費量")]
    public int enemyBulletDamage = 7;

    [Header("歩行の消費量")]
    public int moveBatteryCost = 2;

    [Header("ライトアタックの消費量")]
    public int lightAttackCost = 10;

    [Header("一本あたりのバッテリー量")]
    public int maxBatteryPerUnit = 100;

    [Header("バッテリー本数")]
    public int batteryCount = 4;
}