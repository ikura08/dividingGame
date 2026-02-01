using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSelect : MonoBehaviour, ITrigger
{
    public int stageNumber;
    public void CoreTrigger()
    {
        string sceneName = "Stage" + stageNumber;
        // SceneManager.LoadScene(sceneName);
        SceneManagerScr.Instance.FadeAndLoad(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
