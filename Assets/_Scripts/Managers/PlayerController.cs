using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    #region Variables

    public float speed;
    public float jumpForce;
    public float moveTime;
    public float attackDelay = 1f;
    public float invulnerableTimer = 10.0f;

    private Rigidbody2D rbPlayer;
    private Vector3 prevPosition;
    private Vector3 nextPosition;
    private bool isMoving;
    private bool isAttacking;

    private int direction;
    private bool isInvulnerable = false; //Usar esta variable para ver si el personaje tiene activo el escudo.

    [SerializeField]
    private UnityEngine.UI.Text scoreController;

    public float raycastViewRange = 90.0f;
    public float raycastHitRange = 5.0f;
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
        Vector3 rayOriginPosition = new Vector3(this.transform.position.x, this.transform.position.y - .5f, this.transform.position.z);
        Debug.DrawRay(rayOriginPosition, new Vector2(this.transform.localScale.x * raycastHitRange, 0) , Color.red);
        Debug.DrawRay(this.transform.position, new Vector2(-this.transform.localScale.x, 0) * raycastViewRange, Color.blue);
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
    /// <summary>
    /// Movve Left Function
    /// </summary>
    /// <returns></returns>
    public bool MoveLeft()
    {
        if (direction == 1) FlipSprite();
        
        rbPlayer.velocity = new Vector2(speed * moveTime * -1, rbPlayer.velocity.y);
        isMoving = true;

        return true;
    }

    /// <summary>
    /// Move Right Function
    /// </summary>
    /// <returns></returns>
    public bool MoveRight()
    {
        if (direction == -1) FlipSprite();

        rbPlayer.velocity = new Vector2(speed * moveTime, rbPlayer.velocity.y);
        isMoving = true;

        return true;
    }
    /// <summary>
    /// Jump Function
    /// </summary>
    /// <returns></returns>
    public bool Jump()
    {
        rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, 0f);
        rbPlayer.AddForce(new Vector2(0f, jumpForce));

        isMoving = true;

        return true;
    }

    /// <summary>
    /// Attack Function
    /// </summary>
    /// <returns></returns>
    public bool Attack()
    {
        //Check closest enemy to player
        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(this.transform.localScale.x, 0));
        Vector3 rayOriginPosition = new Vector3(this.transform.position.x, this.transform.position.y -.5f, this.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayOriginPosition, new Vector2(-this.transform.localScale.x, -.5f));
        RaycastHit2D hitEnemy = Physics2D.Raycast(this.transform.position, new Vector2(this.transform.localScale.x, 0));

        if (hit.collider.tag == "Enemy") {
            //Flip sprite towards enmey
            FlipSprite();
        }
        //Perform Hit with a raycast
        if (hitEnemy.collider.tag == "Enemy" && Mathf.Abs(Vector3.Distance(hitEnemy.collider.gameObject.transform.position, this.transform.position)) <= raycastHitRange) {
            //if the raycast hits an enemy destroy the enemy gameobject (maybe send a signal to play death animation?)
            hitEnemy.collider.gameObject.GetComponent<EnemyBase>().Die(); //Puede que no funciones y haya que mirar el tipo de Enemigo al que accedemos.
        }
        //if it doesnt hit anything then keep going.

        isAttacking = true;

        return true;
    }

    /// <summary>
    /// Defend Function
    /// </summary>
    /// <returns></returns>
    public bool Defend()
    {
        StartCoroutine(ShieldTimer(invulnerableTimer));
        return true;
    }

    /// <summary>
    /// Flips Character Sprite.
    /// </summary>
    protected void FlipSprite()
    {
        if (direction == 1) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;

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

    /// <summary>
    /// Timer that sets a delay on attack.
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator WaitAttack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
    }

    /// <summary>
    /// Timer that turns on and off the shield.
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator ShieldTimer(float seconds) {
        isInvulnerable = true;
        yield return new WaitForSeconds(seconds);
        isInvulnerable = false;
    }

    #endregion
}
