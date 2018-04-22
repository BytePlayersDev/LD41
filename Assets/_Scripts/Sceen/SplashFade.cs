using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour {

    public Image logo;
    
    IEnumerator Start()
    {
        logo.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene("Menu");
    }

    void FadeIn()
    {
        logo.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        logo.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
