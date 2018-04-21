using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float moveTime;

    private Rigidbody2D rb;

    private bool canMove;

	// Use this for initialization
	void Start ()
    {
        rb = this.GetComponent<Rigidbody2D>();

        canMove = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public bool MoveLeft()
    {
        return true;
    }

    public bool MoveRight()
    {
        StartCoroutine(MoveVertically(1));
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

    #region Coroutines
    // This courutine runs always, increasing the value of "Critic Percentage"
    IEnumerator MoveVertically(int dir)
    {
        if (dir == 1)
        {
            canMove = true;
            rb.velocity = new Vector2(speed * moveTime, rb.velocity.y);
        }
        else
        {
            canMove = true;
            rb.velocity = new Vector2(speed * moveTime * -1, rb.velocity.y);
        }

        yield return new WaitForSeconds(moveTime);
        canMove = false;
    }

    #endregion
}
