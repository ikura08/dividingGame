using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Inspectorで中身を見られるようにする
public class CoreGroup
{
    public List<GameObject> cores = new List<GameObject>();
}
public class CoreProvider : MonoBehaviour
{
    public GameObject corePrefab;
    public Vector3 point;
    int count = 5;
    float distance;  //生成するメタルの距離
    public CoreGroup[] coreLists;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ProvidingX(Vector3 size, int number)
    {
        distance = size.x;
        point = new Vector3(point.x + distance, point.y, -0.5f);
        corePrefab.transform.localScale = size;
        GameObject core = Instantiate(corePrefab, point, Quaternion.identity);
        coreLists[number].cores.Add(core);
        yield return null;
    }

    public IEnumerator ProvidingY(Vector3 size, int number)
    {
        distance = size.x;
        point = new Vector3(point.x, point.y + distance, -0.5f);
        corePrefab.transform.localScale = size;
        GameObject core = Instantiate(corePrefab, point, Quaternion.identity);
        coreLists[number].cores.Add(core);
        yield return null;
    }

    public IEnumerator Blinking(int number)
    {
        Hide(number);
        // AudioController.Instance.Sound1();
        yield return new WaitForSeconds(0.07f);

        Show(number);
        yield return new WaitForSeconds(0.8f);

        Hide(number);
        // AudioController.Instance.Sound1();
        yield return new WaitForSeconds(0.07f);

        Show(number);
        yield return new WaitForSeconds(0.8f);
        
        Hide(number);
        // AudioController.Instance.Sound1();
        yield return new WaitForSeconds(0.07f);

        Show(number);
        yield return new WaitForSeconds(0.8f);
        
        Hide(number);
        // AudioController.Instance.Sound1();
        yield return new WaitForSeconds(0.07f);

        Show(number);
        yield return new WaitForSeconds(0.8f);

        DestroyAllCores(number);

        yield return null;
    }

    public void DestroyAllCores(int number)
    {
        Debug.Log("実行された");
        foreach (GameObject core in coreLists[number].cores)
        {
            if (core != null)
            {
                Destroy(core);
            }
        }
        coreLists[number].cores.Clear();
    }

    public void Show(int number)
    {
        for (int i = 0; i < coreLists[number].cores.Count; i++)
        {
            for (int j = 0; j < coreLists[number].cores[i].transform.childCount; j++)
            {
                Renderer child = coreLists[number].cores[i].transform.GetChild(j).GetComponent<Renderer>();
                child.enabled = true;
            }
        }
    }
    public void Hide(int number)
    {
        for (int i = 0; i < coreLists[number].cores.Count; i++)
        {
            for (int j = 0; j < coreLists[number].cores[i].transform.childCount; j++)
            {
                Renderer child = coreLists[number].cores[i].transform.GetChild(j).GetComponent<Renderer>();
                child.enabled = false;
            }
        }
    }
}
