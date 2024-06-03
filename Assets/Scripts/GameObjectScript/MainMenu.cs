using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void ChangeScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
