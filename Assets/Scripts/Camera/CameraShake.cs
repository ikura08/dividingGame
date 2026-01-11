using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Shake(float duration, float intensity)
    {
        StopAllCoroutines(); // 連続で当たった時のためにリセット
        StartCoroutine(DoShake(duration, intensity));
    }

    private IEnumerator DoShake(float duration, float intensity)
    {
        Vector3 originalPos = transform.localPosition;
        float time = 0f;

        while (time < duration)
        {
            // ランダムに位置をずらす
            float x = Random.Range(-0.1f, 0.1f) * intensity;
            float y = Random.Range(-0.1f, 0.1f) * intensity;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, originalPos.z);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos; // 元の位置に戻す
    }
}