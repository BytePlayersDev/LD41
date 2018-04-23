using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTest : MonoBehaviour {

    public Animator anim;
    public float seconds;
        
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ShieldTimer(seconds));
	}

    IEnumerator ShieldTimer(float seconds)
    {
        float fixedShield = seconds / 12;

        for (int i = 13; i > 0; --i)
        {
            anim.SetFloat("porcentaje", i);
            yield return new WaitForSeconds(fixedShield);
        }
    }
}
