using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region UI Variables
    [SerializeField] private GameObject window;
    [SerializeField] private CameraScript cs;
    [SerializeField] private LevelManager lm;

    private Text windowTxt;    
    private bool gameOverActivated = false;
    #endregion
    #region Unity Methods
    private void Start()
    {
        if (window == false) {
            Debug.LogError("Pause menu is null in " + this.gameObject.name);
        }
    }

    #endregion

    #region UI Functions
    public void PauseButtonPressed()
    {
        if (!gameOverActivated)
        {
            windowTxt = window.transform.GetChild(1).GetComponent<Text>();
            windowTxt.text = "PAUSE";

            if (!window.activeInHierarchy)
            {
                window.SetActive(true);
                Time.timeScale = 0;
                cs.SetGamePaused(true);
            }
            else
            {
                window.SetActive(false);
                cs.SetGamePaused(false);
                Time.timeScale = 1;
            }
        }
    }

    public void ShowGameOver()
    {
        gameOverActivated = true;

        windowTxt = window.transform.GetChild(2).GetComponent<Text>();
        windowTxt.text = "Game over";

        if (!window.activeInHierarchy)
        {
            window.SetActive(true);
            Time.timeScale = 0;
            cs.SetGamePaused(true);
        }
    }

    public void RestartButtonPressed() {
        lm.LoadLevel();
    }

    public void MenuButtonPressed() {
        lm.LoadMainMenu();
    }
    
    #endregion
}
