using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    public void LoadStage(string name)
    {
        SceneManager.LoadScene(name);
    }
}