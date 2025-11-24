using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float amplitude = 0.1f; // 揺れの大きさ
    public float frequency = 0.7f;    // 揺れる速さ

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0, y, 0);
    }
}
