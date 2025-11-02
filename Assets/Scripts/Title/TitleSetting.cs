using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleSetting : MonoBehaviour
{
    public GameObject jumpCore;
    public GameObject warpCore1;
    public GameObject warpCore2;
    public GameObject fakePlayer;
    public GameObject stairCore;
    ITrigger trigger;
    private bool isAppearance = false;
    private CoreStair coreStairScr;
    int lastnumber = -1; //直前にランダムで出たnumberの値
    // Start is called before the first frame update
    void Start()
    {
        trigger = jumpCore.GetComponent<ITrigger>();
        coreStairScr = stairCore.GetComponent<CoreStair>();

        coreStairScr.destroyTime = 1.0f;

        // jumpCore.SetActive(false);
        // warpCore1.SetActive(false);
        // warpCore2.SetActive(false);
        // fakePlayer.SetActive(false);
        // stairCore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAppearance == false)
        {
            int number = Random.Range(0, 3);
            if (number == lastnumber) number = (number + 1) % 3;

            switch (number)
            {
                case 0:
                    StartCoroutine(JumpCoreAppearance());
                    break;
                case 1:
                    StartCoroutine(WarpCoreAppearance());
                    break;
                case 2:
                    StartCoroutine(ProvideCoreAppearance());
                    break;
            }

            lastnumber = number;
            isAppearance = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManagerScr.Instance.FadeAndLoad("Tutrial");
        }
    }

    private IEnumerator JumpCoreAppearance()
    {
        yield return new WaitForSeconds(0.2f);

        trigger = jumpCore.GetComponent<ITrigger>();
        jumpCore.SetActive(true);
        yield return new WaitForSeconds(1.8f);

        trigger.CoreTrigger();
        yield return new WaitForSeconds(1f);

        trigger.CoreTrigger();
        yield return new WaitForSeconds(1.5f);

        // jumpCore.SetActive(false);

        isAppearance = false;
    }

    private IEnumerator WarpCoreAppearance()
    {
        yield return new WaitForSeconds(0.2f);

        trigger = warpCore1.GetComponent<ITrigger>();
        warpCore1.SetActive(true);
        warpCore2.SetActive(true);
        fakePlayer.SetActive(true);
        yield return new WaitForSeconds(1.3f);

        trigger.CoreTrigger();
        yield return new WaitForSeconds(1f);

        trigger = warpCore2.GetComponent<ITrigger>();
        trigger.CoreTrigger();
        yield return new WaitForSeconds(1.5f);

        // warpCore1.SetActive(false);
        // warpCore2.SetActive(false);
        // fakePlayer.SetActive(false);

        isAppearance = false;
    }

    private IEnumerator ProvideCoreAppearance()
    {
        yield return new WaitForSeconds(0.2f);

        trigger = stairCore.GetComponent<ITrigger>();
        stairCore.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        trigger.CoreTrigger();
        yield return new WaitForSeconds(1.5f);

        // stairCore.SetActive(false);

        isAppearance = false;
    }
    
}
