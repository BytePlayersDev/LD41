﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    #region Variables
    private GameManager gameManager;
    public Animator anim;
    public AudioClip jumpSound;
    
    public float speed;
    public float jumpForce;
    public float moveTime;
    public float attackDelay = 1f;
    public float invulnerableTimer = 1.0f;

    private Rigidbody2D rbPlayer;
    private AudioSource aSource;
    private Vector3 prevPosition;
    private Vector3 nextPosition;
    private bool isMoving;
    private bool isAttacking;
    private bool isJumping;
    private bool facingRight = false;

    private int direction;
    private bool isInvulnerable = false; //Usar esta variable para ver si el personaje tiene activo el escudo.
    private bool isEnemyDetected = false;
    //Attack/Shooting Variables
    public GameObject bulletPrf;
    public float bulletSpeed;
    public Transform bulletSpawn;
    public Transform bulletParent;

    //Scores
    [SerializeField]
    private GameObject ScorePlayer;
    [SerializeField]
    private UnityEngine.UI.Text ScorePlayerCanvas;

    public float raycastViewRange = 90.0f;
    public float raycastHitRange = 5.0f;


    public GameObject Shield;
    #endregion

    #region Functions

    // Use this for initialization
    void Start ()
    {
        direction = 1;
        isMoving = false;

        rbPlayer = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        aSource = GetComponent<AudioSource>();

    }
    public void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isAttacking", isAttacking);

        Vector3 rayOriginPosition = new Vector3(this.transform.position.x, this.transform.position.y - .5f, this.transform.position.z);
        //Debug.DrawRay(rayOriginPosition, new Vector2(Vector2.right.x * raycastHitRange, 0) , Color.red);
        Debug.DrawRay(rayOriginPosition, new Vector2(-transform.localScale.x, 0) * raycastViewRange, Color.blue);

        if (!isInvulnerable) //FIXME: Check if Enemy is in the same position or close to it.
        {
            DetectEnemyBack();
        }

    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    private void DetectEnemyBack() {
        Vector3 rayOriginPosition = new Vector3(this.transform.position.x, this.transform.position.y - .5f, this.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayOriginPosition, new Vector2(-transform.localScale.x, 0));
        // RaycastHit2D hitEnemy = Physics2D.Raycast(rayOriginPosition, Vector2.right);

        if(hit.collider != null)
            if (hit.collider.tag == "Enemy")
            {
                //Flip sprite towards enmey
                Debug.Log("FlipSprite");
                FlipSprite();
                isEnemyDetected = true;
            } else {
                isEnemyDetected = false;
            }
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
        isMoving = true;
        isJumping = true;

        rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, 0f);
        rbPlayer.AddForce(new Vector2(0f, jumpForce));

        aSource.PlayOneShot(jumpSound);

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
        isAttacking = true;
        Shoot();
        StartCoroutine(WaitAttack(attackDelay));

        return true;
    }
    private void Shoot() {

        var bullet = (GameObject)Instantiate(bulletPrf, bulletSpawn.position, bulletSpawn.rotation);
        //bullet.transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        if(facingRight)
            bullet.GetComponentInChildren<SpriteRenderer>().flipX = true;
        bullet.transform.SetParent(bulletParent);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * transform.localScale.x,0) * bulletSpeed * 10);


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
        facingRight = !facingRight;
        direction *= -1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Waypoint" && isMoving)
        {
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
            isMoving = false;
            isJumping = false;
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
            showAndCalculatedPlayerScore(20);
        }
    }
    public void showAndCalculatedPlayerScore(int score)
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
        if(Shield != null)
        {
            Shield.SetActive(true);
        }
        showAndCalculatedPlayerScore(10);
        Physics2D.IgnoreLayerCollision(2, 8, true);
        yield return new WaitForSeconds(seconds);
        isInvulnerable = false;
        if (Shield != null)
        {
            Shield.SetActive(false);
        }
        Physics2D.IgnoreLayerCollision(2, 8, false);
    }

    #endregion
}
