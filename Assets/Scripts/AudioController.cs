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
        cutoffs[0] = 500f;
        cutoffs[1] = 10000f;
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
}
