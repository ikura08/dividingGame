using UnityEngine;

public class PlayerBatteryUsage : MonoBehaviour
{
    public float batteryConsumeInterval = 0.1f;
    float batteryTimer = 0f;
    public int moveBatteryCost = 1;

    PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (movement.currentDirection != Vector2.zero)
        {
            Debug.Log("動いた");
            batteryTimer += Time.fixedDeltaTime;

            if (batteryTimer >= batteryConsumeInterval)
            {
                batteryTimer = 0;
                BatteryController.Instance.UseBattery(moveBatteryCost);
            }
        }
        else
        {
            batteryTimer = 0;
        }
    }
}
