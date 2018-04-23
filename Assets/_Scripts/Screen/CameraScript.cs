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
    public float firstLevelSpeed = 500;
    private bool firstLevelIncremented = false;
    public float secondLevelSpeed = 1000;
    private bool secondLevelIncremented = false;
    public float thirdLevelSpeed = 1500;
    private bool thirdLevelIncremented = false;
    public float increment = 0.1f;
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
        if (player.GetComponent<PlayerController>().score >= firstLevelSpeed && player.GetComponent<PlayerController>().score < secondLevelSpeed && firstLevelIncremented == false) {
            firstLevelIncremented = true;
            if(baseSpeed != baseSpeed + increment)
                baseSpeed = increment;
        }
        if (player.GetComponent<PlayerController>().score >= secondLevelSpeed && player.GetComponent<PlayerController>().score < thirdLevelSpeed && secondLevelIncremented == false)
        {
            secondLevelIncremented = true;
            if (baseSpeed != baseSpeed + increment)
                baseSpeed += increment;
        }
        if (player.GetComponent<PlayerController>().score >= thirdLevelSpeed && thirdLevelIncremented == false)
        {
            thirdLevelIncremented = true;
            if (baseSpeed != baseSpeed + increment)
                baseSpeed += increment;
        }

    }



    public void SetGamePaused(bool _gamePaused) {
        isGamePaused = _gamePaused;
    }
}
