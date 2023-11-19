using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameOptionsScript : OptionsMenuScript
{
    // Start is called before the first frame update
    public void BackButton()
    {
        Time.timeScale = 1;
    }
    public void MainMenuButton()
    {
        
        Globals.lives = 3;
            Globals.Level = 1;
            Globals.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
