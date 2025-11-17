using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioClip sound;
    AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Sound1()
    {
        Debug.Log("音楽再生");
        audioSource.PlayOneShot(sound);
    }
}
