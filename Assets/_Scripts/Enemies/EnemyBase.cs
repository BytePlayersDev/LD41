using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    
    protected enum State {
        Static,
        Patrol,
        Chasing,
        Shooting
    }

    [SerializeField]
    protected State currentState;

    #region Variables
    //Rigidbody2D rb;

    //Nodes for patrol behavior
    //This is set for only 2 waypoints
    public Transform[] waypoints;
    [SerializeField] protected GameManager gm;
    [SerializeField]
    protected int waypointID;
    protected bool samePosition;
    public float secondsToWait = 0.5f;

    [HideInInspector]
    public Transform activeNode;

    public float moveSpeed;
    protected float decreaseSpeedFactor = 50;
    protected Vector2 direction;
    //Related to Player variables
    public GameObject player;

    //Graphic varibles
    private bool facingRight;
    //TODO: Add animator
    //TODO: Add enemy sprite

    //Attribute variables
    protected bool isAlive;
    protected bool isAttacking;
    

    #endregion

    #region Custom Functions
    /// <summary>
    /// Flip Enemy Sprite
    /// </summary>
    protected void FlipSprite() {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        direction *= -1;
        
    }
    /// <summary>
    /// Changes Waypoint
    /// </summary>
    protected void ChangeWaypoint() {
        if (waypointID == 0){

            waypointID = 1;
            facingRight = false;
            FlipSprite();
        } else {
            waypointID = 0;
            facingRight = true;
            FlipSprite();
        }
    }

    /// <summary>
    /// Displays enemy death
    /// </summary>
    public void Die() {

        //TODO: Destroy GameObject and children
        //TODO: Play Death animation
        isAlive = false;
        Destroy(this.gameObject.transform.parent);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")  {

            if( player.GetComponent<PlayerController>().getIsInvulnerable() == false)
             gm.Death();
            
        }


    }



    #endregion

}
