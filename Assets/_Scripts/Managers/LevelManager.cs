using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    #region Custom Functions

    // Loads the Level Scene
    public void LoadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
    
    // Loads the Main Menu Scene
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // Loads the Credits Scene
    public void LoadCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }

    // Closes the game
    public void Exit()
    {
        Application.Quit();
    }

    #endregion
}
