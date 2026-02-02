using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class SelectStage : MonoBehaviour
{
    public GameObject[] clearStageBack;
    public GameObject[] movingLight;
    public Button[] buttons;
    public GameObject[] signs;
    public GameObject[] newSigns;
    public static int nextStage =2;
    public static int clearCount = nextStage -1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(nextStage);
        // stageLoaderScr = GetComponent<StageLoader>();
        foreach (GameObject obj in clearStageBack)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in movingLight)
        {
            obj.SetActive(false);
        }
        foreach (Button obj in buttons)
        {
            obj.gameObject.SetActive(false);
        }
        foreach (GameObject obj in signs)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in newSigns)
        {
            obj.SetActive(false);
        }

        // クリア数分だけ表示
        for (int i = 0; i <= clearCount && i < clearStageBack.Length; i++)
        {
            Debug.Log(i);
            clearStageBack[i].SetActive(true);
            buttons[i].gameObject.SetActive(true);

            int stageIndex = i + 1;
            // buttons[i].onClick.AddListener(() => LoadStage(stageIndex));
        }

        for (int i = 0; i <= clearCount - 1 && i < clearStageBack.Length; i++)
        {
            signs[i].SetActive(true);

            int stageIndex = i + 1;
            // buttons[i].onClick.AddListener(() => LoadStage(stageIndex));
        }

        if (movingLight[nextStage - 1] != null)
        {
            movingLight[nextStage - 1].SetActive(true);
            newSigns[nextStage - 1].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // void LoadStage(int stageNumber)
    // {
    //     string sceneName = "Stage" + stageNumber;
    //     // SceneManager.LoadScene(sceneName);
    //     SceneManagerScr.Instance.FadeAndLoad(sceneName);
    // }
}
