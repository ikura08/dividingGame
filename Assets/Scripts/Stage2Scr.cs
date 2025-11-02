using UnityEngine;
using UnityEngine.UI;

public class Stage2Scr : MonoBehaviour
{
    // [SerializeField]
    // private Slider sizeSlider;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject firstArea;
    public bool isFirstArea = true;
    [SerializeField]
    private GameObject secondArea;
    public bool isSecondArea = false;
    [SerializeField]
    private GameObject thirdArea;
    public bool isThirdArea = false;
    [SerializeField]
    private int currentArea;
    Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        targetPos = new Vector3(player.transform.position.x, -0.2f, -1f);
        // cam = Camera.main;
        // if (sizeSlider != null)
        // {
        //     sizeSlider.onValueChanged.AddListener(UpdateCameraSize);
        //     UpdateCameraSize(sizeSlider.value); // 初期反映
        // }
    }

    void Update()
    {
        switch (currentArea)
        {
            case 1: // First area
                targetPos = new Vector3(player.transform.position.x, -0.2f, -1.5f);
                break;
            case 2: // Second area
                targetPos = new Vector3(player.transform.position.x, 4.7f, -1.5f);
                break;
            case 3: // Third area
                targetPos = new Vector3(player.transform.position.x, 6.6f, -1.5f);
                break;
        }

        // transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.3f);

    }

    // void UpdateCameraSize(float value)
    // {
    //     cam.orthographicSize = value * 1.2f + 4;
    // }
    
    public void SetArea(int area)
    {
        currentArea = area;
    }
}
