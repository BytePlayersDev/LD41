using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFade : MonoBehaviour
{
    public GameObject logo;
    public float fadeSeconds = 2f;

    public bool fadeIn = false;
    public bool fadeOut = false;

    private Image img;

    void Start()
    {
        if (!logo.activeInHierarchy) logo.SetActive(true);
        img = logo.GetComponent<Image>();

        if (fadeIn)
        {
            StartCoroutine(FInCoroutine(fadeSeconds));
        }
        else
        {
            StartCoroutine(FOutCoroutine(fadeSeconds));
        }
    }
    
    IEnumerator FInCoroutine(float seconds)
    {
        img.canvasRenderer.SetAlpha(0.0f);

        img.CrossFadeAlpha(1.0f, seconds, false);
        yield return new WaitForSeconds(0);
    }

    IEnumerator FOutCoroutine(float seconds)
    {
        img.canvasRenderer.SetAlpha(1.0f);

        img.CrossFadeAlpha(0.0f, seconds, false);
        yield return new WaitForSeconds(0);
    }
}
