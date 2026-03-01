using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioMixer mainMixer;
    public AudioClip sound;
    AudioSource audioSource;
    [Header("BGM Settings")]
    public AudioSource bgmSource;
    public AudioSource clearbgmSource;

    public string cutoffParam = "BGM_Cutoff";
    private Coroutine reBGMCoroutine;
    public float changeDuration = 0.5f;
    private float[] cutoffs = new float[3];

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cutoffs[0] = 9000f;
        cutoffs[1] = 15000f;
        cutoffs[2] = 22000f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Sound1()
    {
        audioSource.PlayOneShot(sound);
    }

    public void BGMFillter(int level)
    {
        if (reBGMCoroutine != null) 
        {
            StopCoroutine(reBGMCoroutine);
        }
        reBGMCoroutine = StartCoroutine(UpdateBGM(cutoffs[level]));
    }

    IEnumerator UpdateBGM(float targetValue)
    {
        float startCutoffValue;
        if (!mainMixer.GetFloat(cutoffParam, out startCutoffValue))
        {
            startCutoffValue = 22000f; 
        }

        float time = 0f;

        while (time < changeDuration)
        {
            time += Time.deltaTime;
            float t = time / changeDuration;

            // 2. Lerpで値を補間する
            float cutoffValue = Mathf.Lerp(startCutoffValue, targetValue, t);
            mainMixer.SetFloat(cutoffParam, cutoffValue);

            yield return null;
        }

        // 3. 最後に値を確実に固定する
        mainMixer.SetFloat(cutoffParam, targetValue);
    }

    public void FadeOutBGM(float duration)
    {
        if (reBGMCoroutine != null) 
        {
            StopCoroutine(reBGMCoroutine);
        }
        // 音量の方をフェードアウトさせるコルーチンを開始
        StartCoroutine(FadeOutCoroutine(duration));
    }

    IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = bgmSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0, time / duration);
            yield return null;
        }

        bgmSource.volume = 0;
        bgmSource.Stop(); // 完全に消えたら停止
    }
    public void PlayClearBGM()
    {
        StartCoroutine(CrossFade());
    }

    IEnumerator CrossFade()
    {
        Debug.Log("音変わった");
        clearbgmSource.volume = 0;
        clearbgmSource.Play();

        float time = 0;
        float startVol = bgmSource.volume;

        // 2. 同時に音量を操作（クロスフェード）
        while (time < changeDuration)
        {
            time += Time.unscaledDeltaTime;
            float ratio = time / changeDuration;
            
            bgmSource.volume = Mathf.Lerp(startVol, 0, ratio); // 元の曲を下げ
            clearbgmSource.volume = Mathf.Lerp(0, startVol, ratio); // 新しい曲を上げ
            yield return null;
        }

        bgmSource.Stop();
        Debug.Log("変わり終わった");
    }
}
