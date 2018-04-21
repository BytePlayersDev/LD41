using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float speed;
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.up * speed / 100; 
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Player enetered trigger.");
            //TODO: Respawn player
        }
    }
}
