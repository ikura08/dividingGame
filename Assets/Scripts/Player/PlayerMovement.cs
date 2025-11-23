using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    [HideInInspector] public Vector2 currentDirection = Vector2.zero;
    public Vector2 lastDirection = Vector2.right;
    Rigidbody2D Prb;

    void Start()
    {
        Prb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 入力
        if (Input.GetKey(KeyCode.A))
            currentDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            currentDirection = Vector2.right;
        else
            currentDirection = Vector2.zero;

        if (currentDirection != Vector2.zero)
            lastDirection = currentDirection;

        if (transform.position.y < -5f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // 移動処理
        transform.position += (Vector3)(currentDirection * moveSpeed * Time.deltaTime);
    }
}
