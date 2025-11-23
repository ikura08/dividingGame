using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPhaseController : MonoBehaviour
{
    [SerializeField] private Image blackPanel;
    [SerializeField] private Text outText;

    int phase = 2;
    float timer = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManagerScr.Instance.FadeAndLoad(current.name);
        }

        if (phase == 0) FadeOutPhase();
        else if (phase == 1) FadeInPhase();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Poison"))
        {
            HitPoison();
        }
    }

    public void HitPoison()
    {
        outText.gameObject.SetActive(true);
        phase = 0;
        timer = 0;
    }

    void FadeOutPhase()
    {
        timer += Time.deltaTime;

        Color c = blackPanel.color;
        c.a = Mathf.Lerp(0f, 1f, timer * 4);
        blackPanel.color = c;

        if (timer >= 0.25f)
        {
            timer = 0;
            transform.position = Vector3.zero;
            phase = 1;
            outText.gameObject.SetActive(false);
        }
    }

    void FadeInPhase()
    {
        timer += Time.deltaTime;

        Color c = blackPanel.color;
        c.a = Mathf.Lerp(1f, 0f, timer * 4);
        blackPanel.color = c;

        if (timer >= 0.25f)
        {
            phase = 2;
            timer = 0;
        }
    }
}
