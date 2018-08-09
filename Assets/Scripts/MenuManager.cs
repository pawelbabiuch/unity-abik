using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public FadeUI fadeUI;

    public void OpenMenu()
    {
        fadeUI.FadeIn();
        Invoke("ChangeTimeScale", fadeUI.fadeTime + fadeUI.startDelay);
    }

    public void Return()
    {
        Time.timeScale = 1;
        fadeUI.FadeOut();
    }

    private void ChangeTimeScale()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
