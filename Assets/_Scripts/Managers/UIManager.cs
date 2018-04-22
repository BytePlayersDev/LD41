using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    #region UI Variables
    [SerializeField] private GameObject pauseWindow;

    #endregion
    #region Unity Methods
    private void Start()
    {
        if (pauseWindow == false) {
            Debug.LogError("Pause menu is null in " + this.gameObject.name);
        }
    }

    #endregion

    #region UI Functions
    public void PauseButtonPressed() {
        if (!pauseWindow.activeInHierarchy){
            pauseWindow.SetActive(true);
            Time.timeScale = 0;
        }
        else{
            pauseWindow.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void RestartButtonPressed() {
        //if (Time.timeScale != 1)
        //    Time.timeScale = 1;
        //TODO: restart game Scene.

    }

    public void MenuButtonPressed() {
        //if (Time.timeScale != 1)
        //    Time.timeScale = 1;
        //TODO: load Menu Scene.
    }
    
    #endregion
}
