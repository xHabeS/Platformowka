using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
