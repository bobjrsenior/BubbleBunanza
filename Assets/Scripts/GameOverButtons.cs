using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void StartButton()
    {
        GlobalVariables.instance.ResetData();
        SceneManager.LoadScene(1);
    }

    public void GiveUpButton()
    {
        SceneManager.LoadScene(0);
    }
}
