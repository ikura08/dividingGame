using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Slider sizeSlider;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (sizeSlider != null)
        {
            sizeSlider.onValueChanged.AddListener(UpdateCameraSize);
            UpdateCameraSize(sizeSlider.value); // 初期反映
        }
    }

    void UpdateCameraSize(float value)
    {
        cam.orthographicSize = value * 1.2f + 4;
    }
}
