using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject GameManager;
    public float speed;
    private float baseSpeed;
    public float speedFactor;
    private bool isGamePaused = false;

    private bool isAlive = true;
    [SerializeField] Transform playerCheck;
    public Transform player;
    // Update is called once per frame
    private void Start()
    {
        baseSpeed = speed;
    }
    void Update ()
    {
        if (isGamePaused)
            return;
        if (isAlive) this.transform.position += Vector3.up * speed / 100;
        if (player.position.y >= playerCheck.position.y)
        {
            speed *= speedFactor;
        }
        else {
            if (speed != baseSpeed)
                speed = baseSpeed;
        }
        
	}



    public void SetGamePaused(bool _gamePaused) {
        isGamePaused = _gamePaused;
    }
}
