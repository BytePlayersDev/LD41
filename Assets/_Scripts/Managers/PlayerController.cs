using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    #region Variables
    private GameManager gameManager;
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

    //Scores
    [SerializeField]
    private GameObject ScorePlayer;
    [SerializeField]
    private UnityEngine.UI.Text ScorePlayerCanvas;

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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void Update()
    {
        Vector3 rayOriginPosition = new Vector3(this.transform.position.x, this.transform.position.y - .5f, this.transform.position.z);
        Debug.DrawRay(rayOriginPosition, new Vector2(Vector2.right.x * raycastHitRange, 0) , Color.red);
        Debug.DrawRay(rayOriginPosition, new Vector2(Vector2.left.x, 0) * raycastViewRange, Color.blue);
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
        RaycastHit2D hit = Physics2D.Raycast(rayOriginPosition, Vector2.left);
        RaycastHit2D hitEnemy = Physics2D.Raycast(rayOriginPosition, Vector2.right);

        if (hit.collider.tag == "Enemy") {
            //Flip sprite towards enmey
            Debug.Log("FlipSprite");

            FlipSprite();
        }
        //Perform Hit with a raycast
        if (hitEnemy.collider.tag == "Enemy")
        {
            Debug.Log("Enemy Hitted");
        }
        if (hitEnemy.collider.tag == "Enemy" && Mathf.Abs(Vector3.Distance(hitEnemy.collider.gameObject.transform.position, this.transform.position)) <= raycastHitRange) {
            //if the raycast hits an enemy destroy the enemy gameobject (maybe send a signal to play death animation?)
            hitEnemy.collider.gameObject.GetComponent<EnemyBase>().Die(); //Puede que no funciones y haya que mirar el tipo de Enemigo al que accedemos.
        }
        //if it doesnt hit anything then keep going.

        isAttacking = true;
        StartCoroutine(WaitAttack(attackDelay));

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
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        transform.transform.GetChild(0).localScale = new Vector2(transform.transform.GetChild(0).transform.localScale.x * -1, transform.transform.GetChild(0).transform.localScale.y);

        direction *= -1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Waypoint" && isMoving)
        {
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
            isMoving = false;
        }
        if (collision.gameObject.tag == "DeathCollider")
        {
            gameManager.Death();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            collision.gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
            showAndCalculatedPlayerScore(200);
        }
    }
    private void showAndCalculatedPlayerScore(int score)
    {
        TextMesh TextMesh = ScorePlayer.GetComponent<TextMesh>();
        if (TextMesh != null)
        {

            TextMesh.text = score.ToString();
            //activar la animacion
            TextMesh.GetComponent<Animator>().enabled = true;
            TextMesh.GetComponent<Animator>().Play("ScoreTextPlayerAnimation", -1, 0f);
        }
        //Aumentar score canvas
        if (ScorePlayerCanvas != null)
        {
            ScorePlayerCanvas.text = (int.Parse(ScorePlayerCanvas.text) + score).ToString();
        }
    }

    public bool getIsInvulnerable() {
        return isInvulnerable;
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
        Physics2D.IgnoreLayerCollision(0, 8, true);
        yield return new WaitForSeconds(seconds);
        isInvulnerable = false;
        Physics2D.IgnoreLayerCollision(0, 8, false);
    }

    #endregion
}
