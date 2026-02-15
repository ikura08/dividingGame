using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance;

    [Header("UI")]
    public Slider[] sliders;
    private Image[] fillImages; // ★自動取得用
    public BatteryConfig config;
    public int currentBattery;
    private int totalMaxBattery;
    private float timer = 0;
    
    [SerializeField] LightController lightController;
    public SoundConfig soundConfig;
    
    [Header("Damage Effect")]
    public Color normalColor = Color.white;
    public Color damageColor = Color.red;
    public float recoverSpeed = 2f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalMaxBattery = config.maxBatteryPerUnit * config.batteryCount;
        currentBattery = totalMaxBattery - 1;

        // ★ 各スライダーから Fill Image を自動取得
        fillImages = new Image[sliders.Length];
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].maxValue = config.maxBatteryPerUnit;
            // スライダーの構造（Fill Area > Fill）からImageを取得
            fillImages[i] = sliders[i].fillRect.GetComponent<Image>();

            if (i < config.batteryCount)
            {
                sliders[i].gameObject.SetActive(true);
                sliders[i].value = config.maxBatteryPerUnit;
            }
            else
            {
                sliders[i].gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (currentBattery > 0 && timer >= 1f)
        {
            UseBattery(config.seondCost);
            timer = 0;
        }

        // nullチェック（Startだと初期化順でエラーになることがあるため、安全策として）
        if (currentBattery <= 0 && !SceneManagerScr.Instance.isGameOver && !GoalManager.Instance.isCleared)
        {
            AudioController.Instance.FadeOutBGM(0.43f);
            SceneManagerScr.Instance.GameOver();
            currentBattery = 1;
        }
    }

    public void UseBattery(int amount)
    {
        currentBattery = Mathf.Max(0, currentBattery - amount);
        UpdateUI();
    }

    public void AddBattery(int amount)
    {
        currentBattery = Mathf.Min(totalMaxBattery, currentBattery + amount);
        UpdateUI();
    }

    void UpdateUI()
    {
        int remaining = currentBattery;

        // ライトとフィルターの更新
        int level = remaining / config.maxBatteryPerUnit; 
        lightController.ChangeLightSize(level);
        AudioController.Instance.BGMFillter(level);

        for (int i = 0; i < sliders.Length; i++)
        {
            if (i >= config.batteryCount)
            {
                sliders[i].gameObject.SetActive(false);
                continue;
            }

            sliders[i].gameObject.SetActive(true);

            if (remaining >= config.maxBatteryPerUnit)
            {
                sliders[i].value = config.maxBatteryPerUnit;
                remaining -= config.maxBatteryPerUnit;
            }
            else
            {
                sliders[i].value = remaining;
                remaining = 0;
            }
        }
    }

    // ★ ダメージ演出の呼び出し（どのスライダーを光らせるか判定）
    public void OnDamage()
    {
        // 現在「満タンではない」一番上のバッテリーを探す
        int targetIndex = 0;
        for (int i = 0; i < config.batteryCount; i++)
        {
            if (sliders[i].value > 0)
            {
                targetIndex = i;
                // ※このループで「一番右（または上）の減っている最中のやつ」を特定
            }
        }
        
        StartCoroutine(FlashDamageColor(targetIndex));
    }

    private IEnumerator FlashDamageColor(int index)
    {
        Image targetFill = fillImages[index];
        targetFill.color = damageColor;

        float t = 0;
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime * recoverSpeed;
            targetFill.color = Color.Lerp(damageColor, normalColor, t);
            yield return null;
        }
        targetFill.color = normalColor;
    }
}