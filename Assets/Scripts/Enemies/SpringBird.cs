using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBird : EnemyBase {

    #region Variables
    private int changeOrder = 1;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
        if (moveSpeed == 0) moveSpeed = 2;
        direction = new Vector2(transform.localScale.x, 0);
        samePosition = false;
        isAlive = true;
        currentState = EnemyBase.State.Patrol;

        if (waypoints == null) Debug.LogError("Assign wayponits to " + this.gameObject.name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyBase.State.Patrol:
                Patrol();
                break;
        }

    }

    #endregion

    #region Custom Functions
    /// <summary>
    /// Simple Patrol Function
    /// </summary>
    private void Patrol()
    {
        if (Mathf.Abs(Vector2.Distance(waypoints[waypointID].position, transform.position)) < .5f && !samePosition) {
            ChangeWaypoint();
        }
        if (!samePosition) {
            Vector2 translation = Vector2.MoveTowards(this.transform.position, waypoints[waypointID].transform.position, moveSpeed / decreaseSpeedFactor);
            transform.position = new Vector2(translation.x, translation.y);
        }
    }
    /// <summary>
    /// Overloaded ChangeWaypoint Function for the springBird
    /// </summary>
    new private void ChangeWaypoint() {
        
        if (waypointID == 0){
            FlipSprite();
            changeOrder = 1;
        }
        if (waypointID == waypoints.Length - 1) {
            FlipSprite();
            changeOrder = -1;
        }
        
        waypointID = (waypointID + changeOrder) % (waypoints.Length);
    }
    #endregion
}
