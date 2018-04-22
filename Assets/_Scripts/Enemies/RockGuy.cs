using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGuy : EnemyBase {
    #region Variables
    private Vector2 translation;
    [SerializeField]private Animator anim;
    private bool isPatrolling = true;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start () {
        if (moveSpeed == 0) moveSpeed = 2;
        direction = new Vector2(transform.localScale.x, 0);
        samePosition = false;
        isAlive = true;
        currentState = State.Patrol;

        if (secondsToWait == 0) secondsToWait = 2;
        if (waypoints == null) Debug.LogError("Assign wayponits to " + this.gameObject.name);
	}

    private void Update(){
        anim.SetBool("isPatrolling", isPatrolling);
    }
    void FixedUpdate () {
        if (Mathf.Abs(Vector2.Distance(waypoints[waypointID].position, transform.position)) < .5f && !samePosition)
        {
            ChangeWaypoint();
            currentState = State.Static;
            isPatrolling = false;
        }

        switch (currentState) {
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
    private void Patrol() {
        if (!samePosition) {
            translation = Vector2.MoveTowards(this.transform.position, waypoints[waypointID].transform.position, moveSpeed / decreaseSpeedFactor);
            transform.position = new Vector2(translation.x, transform.position.y);
        }
    }

    new protected IEnumerator Static()
    {
        yield return new WaitForSeconds(secondsToWait);
        currentState = State.Patrol;
        isPatrolling = true;
    }

    #endregion

}
