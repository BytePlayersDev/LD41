using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject GameManager;
    public float speed;
    private bool isGamePaused = false;

    private bool isAlive = true;

	// Update is called once per frame
	void Update ()
    {
        if (isGamePaused)
            return;
        if (isAlive) this.transform.position += Vector3.up * speed / 100; 
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //isAlive = false;
            //GameManager.GetComponent<GameManager>().Death();
        }
    }

    public void SetGamePaused(bool _gamePaused) {
        isGamePaused = _gamePaused;
    }
}
