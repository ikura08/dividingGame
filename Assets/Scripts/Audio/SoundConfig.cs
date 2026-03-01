using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Configs/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    [Header("つらら衝突音")]
    public AudioClip dropClip;

    [Header("プレイヤージャンプ音")]
    public AudioClip jumpPClip;
    
    [Header("敵ジャンプ音")]
    public AudioClip jumpEClip;
    
    [Header("コアジャンプ音")]
    public AudioClip jumpCClip;
    
    [Header("コアチャージ音")]
    public AudioClip chargeClip;
    
    [Header("コアワープ音")]
    public AudioClip warpClip;
    
    [Header("コア生成音")]
    public AudioClip provideClip;
    
    [Header("コアシーン音")]
    public AudioClip sceneClip;
    
    [Header("プレイヤー銃音")]
    public AudioClip bulletPClip;
    
    [Header("敵銃音")]
    public AudioClip bulletEClip;
    
    [Header("プレイヤー死亡音")]
    public AudioClip diePClip;
    
    [Header("敵死亡音")]
    public AudioClip dieEClip;
    
    [Header("プレイヤーダメージ音")]
    public AudioClip damagePClip;
    
    [Header("コイン音")]
    public AudioClip coinClip;
    
    [Header("コインゲットのクリア音")]
    public AudioClip clearClipCoinGet;
    
    [Header("ノーマルクリア音")]
    public AudioClip clearClipNormal;
}