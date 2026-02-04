using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropCreator : MonoBehaviour
{
    public GameObject dropPrefab;
    public static EnemyDropCreator Instance;
    private float delay = 5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropCreate(Vector2 position)
    {
        Debug.Log("つらら生成準備");
        StartCoroutine(DelayInstantiate(position));
    }
    IEnumerator DelayInstantiate(Vector2 position)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(dropPrefab, position, Quaternion.identity);
    }
}
