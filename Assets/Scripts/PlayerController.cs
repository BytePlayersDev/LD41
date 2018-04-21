﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    
    public GameObject player;
    public GameObject[] waypoints;

    public float speed;
    public float moveTime;
    
    private Rigidbody2D rbPlayer;
    private Vector3 prevPosition;
    private bool isMoving;
    private int direction;

    #endregion

    #region Functions

    // Use this for initialization
    void Start ()
    {
        direction = 1;
        rbPlayer = player.GetComponent<Rigidbody2D>();
        isMoving = false;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            for (int i = 0; i < waypoints.Length; ++i)
            {
                if (Mathf.Abs(Vector2.Distance(waypoints[i].transform.position, player.transform.position)) < .5f)
                {
                    rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
                    MoveWaypoints();
                }
            }
        }
    }

    public bool MoveLeft()
    {
        if (direction == 1) FlipSprite();

        prevPosition = player.transform.position;

        rbPlayer.velocity = new Vector2(speed * moveTime * -1, rbPlayer.velocity.y);
        isMoving = true;

        return true;
    }

    public bool MoveRight()
    {
        if (direction == -1) FlipSprite();

        prevPosition = player.transform.position;

        rbPlayer.velocity = new Vector2(speed * moveTime, rbPlayer.velocity.y);
        isMoving = true;

        return true;
    }

    public bool Jump()
    {
        return true;
    }

    public bool Attack()
    {
        return true;
    }

    public bool Defend()
    {
        return true;
    }

    protected void FlipSprite()
    {
        player.transform.localScale = new Vector2(player.transform.localScale.x * -1, player.transform.localScale.y);
        direction *= -1;
    }

    protected void MoveWaypoints()
    {
        Vector3 diference = player.transform.position - prevPosition;

        waypoints[0].transform.position += diference;
        waypoints[1].transform.position += diference;
        waypoints[2].transform.position += diference;

        isMoving = false;
    }
    
    #endregion

    #region Coroutines

    // This courutine runs always, increasing the value of "Critic Percentage"
    IEnumerator GetValid()
    {
        yield return new WaitForSeconds(moveTime);
    }

    #endregion
}
