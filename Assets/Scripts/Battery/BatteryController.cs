using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance;

    [Header("UI")]
    public Slider[] sliders;
    public BatteryConfig config;
    public int currentBattery;
    private int totalMaxBattery;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 全体最大値をScriptableObjectから算出
        totalMaxBattery = config.maxBatteryPerUnit * config.batteryCount;
        currentBattery = totalMaxBattery;

        // スライダー初期設定
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].maxValue = config.maxBatteryPerUnit;

            if (i < config.batteryCount)
            {
                sliders[i].gameObject.SetActive(true);
                sliders[i].value = config.maxBatteryPerUnit;
            }
            else
            {
                sliders[i].gameObject.SetActive(false); // 使わない分は非表示
            }
        }
    }

    void Update()
    {
        Debug.Log("残量は" + currentBattery, gameObject);
        if (currentBattery <= 0)
        {
            Debug.Log("亡くなった");
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
    
}
