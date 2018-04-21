using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    public float speed;
    public float moveTime;

    private GameObject[] childs;

    private Rigidbody2D rbPlayer;
    private int direction;
    private bool canMove;

    #endregion

    #region Functions

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            childs[i] = gameObject.transform.GetChild(i).gameObject;
        }

        direction = 1;
        rbPlayer = childs[0].GetComponent<Rigidbody2D>();

        canMove = false;
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }
    }
    
    public bool MoveLeft()
    {
        if (direction == 1) FlipSprite();

        rbPlayer.velocity = new Vector2(speed * moveTime * -1, rbPlayer.velocity.y);
        return true;
    }

    public bool MoveRight()
    {
        if (direction == -1) FlipSprite();

        rbPlayer.velocity = new Vector2(speed * moveTime, rbPlayer.velocity.y);
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
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        direction *= -1;
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
