using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            StartCoroutine(Chase());
            isMoving = true;
        }
    }

    IEnumerator Chase()
    {
        Vector3 direction = player.transform.position - transform.position;
        Vector3 unitVector = direction.normalized;
        transform.position += unitVector * 0.2f;

        yield return new WaitForSeconds(0.2f);

        isMoving = false;
    }
}
