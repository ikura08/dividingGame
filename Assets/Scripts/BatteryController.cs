using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance;

    [Header("UI")]
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;

    [Header("バッテリー設定")]
    public int maxBattery = 100;
    public int currentBattery;

    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentBattery = maxBattery;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UseBattery(int amount)
    {
        Debug.Log("バッテリー減少");
        currentBattery -= amount;

        if (currentBattery < 0)
            currentBattery = 0;

        UpdateUI();
    }
    
    public void AddBattery(int amount)
    {
        currentBattery += amount;

        if (currentBattery > maxBattery)
            currentBattery = maxBattery;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (slider1 != null)
        {
            slider1.maxValue = maxBattery;
            slider1.value = currentBattery;
        }
    }
    

}
