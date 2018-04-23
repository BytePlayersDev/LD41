using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour {
        
    void Start()
    {
        StartCoroutine(WaitCredits());
    }

    IEnumerator WaitCredits()
    {
        yield return new WaitForSeconds(40);
        SceneManager.LoadScene("Menu");
    }
}
