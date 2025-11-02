// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
// using UnityEngine.Rendering.PostProcessing;


public class TestPostProcessing : MonoBehaviour
{
    [SerializeField]
    private Volume metalProfile;
    private PostProcessVolume postProcessVolume;
    private Bloom bloom;
    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        bloom = postProcessVolume.profile.GetSetting<Bloom>();
        bloom.intensity.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
