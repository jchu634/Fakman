using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        resetGlobals();
    }
    public void ReloadGame()
    {
        resetGlobals();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    public void resetGlobals()
    {
        Globals.lives = 3;
        Globals.Level = 1;
        Globals.score = 0;
    }
}
