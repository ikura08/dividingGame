using UnityEngine;

public class PlayerAreaTrigger : MonoBehaviour
{
    [SerializeField] private CameraMove cameraMove;

    [SerializeField] private GameObject firstArea;
    [SerializeField] private float firstY;

    [SerializeField] private GameObject secondArea;
    [SerializeField] private float secondY;

    [SerializeField] private GameObject thirdArea;
    [SerializeField] private float thirdY;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == firstArea)
            cameraMove.SetArea(1, firstY);

        if (other.gameObject == secondArea)
            cameraMove.SetArea(2, secondY);

        if (other.gameObject == thirdArea)
            cameraMove.SetArea(3, thirdY);
    }
}
