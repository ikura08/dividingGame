using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance;

    [Header("UI")]
    public Slider[] sliders;
    public BatteryConfig config;
    public int currentBattery;
    private int totalMaxBattery;
    private float timer = 0;
    public int batteryLevel = 0;
    [SerializeField]
    LightController lightController;
    public  SoundConfig soundConfig;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalMaxBattery = config.maxBatteryPerUnit * config.batteryCount;
        currentBattery = totalMaxBattery - 1;

        // batteryLevel = sliders.Length;

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
        timer += Time.deltaTime;
        if (currentBattery > 0 && timer >= 1f)
        {
            UseBattery(config.seondCost);
            timer = 0;
        }

        if (currentBattery <= 0 && SceneManagerScr.Instance.isGameOver == false)
        {
            AudioController.Instance.FadeOutBGM(0.8f);
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

        if (remaining <= 100)
        {
            lightController.ChangeLightSize(0);
            AudioController.Instance.BGMFillter(0);
        }
        else if (remaining <= 200)
        {
            lightController.ChangeLightSize(1);
            AudioController.Instance.BGMFillter(1);
        }
        else if (remaining <= 300)
        {
            lightController.ChangeLightSize(2);
            AudioController.Instance.BGMFillter(2);
        }

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
