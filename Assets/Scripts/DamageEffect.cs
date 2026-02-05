using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    public static DamageEffect Instance;
    private Volume volume;
    private Vignette vignette;

    void Awake()
        {
            // インスタンスの登録
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

    void Start()
    {
        // シーン内のVolumeからVignetteを探す
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }

    public void FlashRed()
    {
        StartCoroutine(DamageFlare());
    }

    IEnumerator DamageFlare()
    {
        Debug.Log("赤くなった");
        // 1. 一瞬で赤くする
        vignette.intensity.value = 1.0f; 

        // 2. じわじわ戻す
        float duration = 0.5f; // 演出時間
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // 徐々に0に近づける
            vignette.intensity.value = Mathf.Lerp(1.0f, 0f, elapsed / duration);
            yield return null;
        }

        vignette.intensity.value = 0f;
    }
}