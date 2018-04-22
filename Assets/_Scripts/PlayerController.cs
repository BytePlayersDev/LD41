﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    #region Variables

    public float speed;
    public float jumpForce;
    public float moveTime;
    
    private Rigidbody2D rbPlayer;
    private Vector3 prevPosition;
    private Vector3 nextPosition;
    private bool isMoving;
    private int direction;

    [SerializeField]
    private UnityEngine.UI.Text scoreController;

    float raycastViewRange = 90.0f;
    float raycastHitRange = 10.0f;
    #endregion

    #region Functions

    // Use this for initialization
    void Start ()
    {
        direction = 1;
        rbPlayer = GetComponent<Rigidbody2D>();
        isMoving = false;
    }
    public void Update()
    {
        Debug.DrawRay(this.transform.position, new Vector2(this.transform.localScale.x, 0) * raycastHitRange, Color.red);
        Debug.DrawRay(this.transform.position, new Vector2(-this.transform.localScale.x, 0) * raycastViewRange, Color.blue);
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }

    public bool MoveLeft()
    {
        if (direction == 1) FlipSprite();
        
        rbPlayer.velocity = new Vector2(speed * moveTime * -1, rbPlayer.velocity.y);
        isMoving = true;

        return true;
    }

    public bool MoveRight()
    {
        if (direction == -1) FlipSprite();

        rbPlayer.velocity = new Vector2(speed * moveTime, rbPlayer.velocity.y);
        isMoving = true;

        return true;
    }

    public bool Jump()
    {
        rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, 0f);
        rbPlayer.AddForce(new Vector2(0f, jumpForce));

        isMoving = true;

        return true;
    }

    public bool Attack()
    {
        //Check closest enemy to player
        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(this.transform.localScale.x, 0));
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(-this.transform.localScale.x, 0));
        RaycastHit2D hitEnemy = Physics2D.Raycast(this.transform.position, new Vector2(-this.transform.localScale.x, 0));

        if (hit.collider.tag == "Enemy") {
            //Flip sprite towards enmey
            FlipSprite();
        }
        //Perform Hit with a raycast
        if (hitEnemy.collider.tag == "Enemy" && Mathf.Abs(Vector3.Distance(hitEnemy.collider.gameObject.transform.position, this.transform.position)) <= raycastHitRange) {
            //if the raycast hits an enemy destroy the enemy gameobject (maybe send a signal to play death animation?)
            hitEnemy.collider.gameObject.GetComponent<EnemyBase>().Die();
        }
        //if it doesnt hit anything then keep going.


        return true;
    }

    public bool Defend()
    {
        return true;
    }

    protected void FlipSprite()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        direction *= -1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Waypoint" && isMoving)
        {
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
            isMoving = false;

            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            collision.gameObject.GetComponent<TilemapCollider2D>().isTrigger = false;
            //TODO aumentar score 200 puntos al saltar
            int score = int.Parse(scoreController.text) + 200;
            scoreController.text = score.ToString();
        }
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
