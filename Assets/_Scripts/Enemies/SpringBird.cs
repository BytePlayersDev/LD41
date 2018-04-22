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

        if (this.GetComponentInChildren<Collider2D>() == null) Debug.LogError("Add collider2D in " + this.gameObject.name);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponentInChildren<Collider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(Vector2.Distance(waypoints[waypointID].position, transform.position)) < .5f && !samePosition)
        {
            ChangeWaypoint();
            if(waypointID %2 != 0)
                currentState = State.Static;

        }
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Static:
                StartCoroutine(Static());
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

    new protected IEnumerator Static()
    {
        yield return new WaitForSeconds(secondsToWait);
        currentState = State.Patrol;
    }
    #endregion
}
