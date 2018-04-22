using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOctopus : EnemyBase {

    #region Variables
    //Shooting variables
    public GameObject bulletPrf;
    public Transform shooter;


    public float bulletSpeed;
    private float lastShot;
    public float delayBetweenShots = 2.0f;
    public Transform bulletParent;
    //AudioSource shootSound;

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
        if (bulletPrf == null) Debug.LogError("Assign a bullet prefab to " + this.gameObject.name);

        if (this.GetComponentInChildren<Collider2D>() == null) Debug.LogError("Add collider2D in " + this.gameObject.name);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponentInChildren<Collider2D>());
    }

    private void Update(){

    }
    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyBase.State.Patrol:
                isAttacking = false;
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
        if (Mathf.Abs(Vector2.Distance(waypoints[waypointID].position, transform.position)) < .5f && !samePosition)
        {
            ChangeWaypoint();
        }

        if (!samePosition)
        {
            Vector2 translation = Vector2.MoveTowards(this.transform.position, waypoints[waypointID].transform.position, moveSpeed / decreaseSpeedFactor);
            transform.position = new Vector2(translation.x, translation.y);
        }

        Shoot();
    }
    /// <summary>
    /// Method for shooting
    /// </summary>
    private void Shoot() {

        if (Time.time - lastShot > delayBetweenShots) {

            var bullet = (GameObject)Instantiate(bulletPrf, shooter.transform.position, Quaternion.identity);
            bullet.transform.SetParent(bulletParent);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.localScale.x, -1)
            * bulletSpeed * 10);

            var bullet2 = (GameObject)Instantiate(bulletPrf, shooter.transform.position, Quaternion.identity);
            bullet2.transform.SetParent(bulletParent);
            bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, -1)
            * bulletSpeed * 10);

            Destroy(bullet, 1.0f);
            Destroy(bullet2, 1.0f);
            //TODO: Play Shoot sound here.
            lastShot = Time.time;
        }
    }

    new private void ChangeWaypoint() {
        waypointID = Random.Range(0, waypoints.Length);
    }
    #endregion
}
