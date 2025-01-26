using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    void Awake()
    {
        GlobalVariables.instance.day = 0;
    }
    public void StartButton()
    {
        GlobalVariables.instance.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
