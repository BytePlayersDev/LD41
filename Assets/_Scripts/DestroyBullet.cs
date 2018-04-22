using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    public int seconds = 2;
    void Update () {
        Destroy(this.gameObject, seconds);	
	}
}
