using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance;

    [Header("UI")]
    public Slider[] sliders;   // ← これで全部まとめて扱える

    [Header("バッテリー設定")]
    public int maxBattery = 400;
    public int currentBattery;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentBattery = maxBattery;

        // 全スライダーをまとめて初期化
        foreach (Slider s in sliders)
        {
            s.maxValue = 100;
            s.value = 100;
        }
    }

    public void UseBattery(int amount)
    {
        currentBattery = Mathf.Max(0, currentBattery - amount);
        UpdateUI();
    }

    public void AddBattery(int amount)
    {
        currentBattery = Mathf.Min(maxBattery, currentBattery + amount);
        UpdateUI();
    }

    void UpdateUI()
    {
        int remaining = currentBattery;

        foreach (Slider s in sliders)
        {
            if (remaining >= 100)
            {
                s.value = 100;
                remaining -= 100;
            }
            else
            {
                s.value = remaining;
                remaining = 0;
            }
        }
    }
}
