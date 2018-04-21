using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float moveTime;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool MoveLeft()
    {
        rb.velocity = new Vector2(speed * moveTime, rb.velocity.y);
        return true;
    }

    public bool MoveRight()
    {
        rb.velocity = new Vector2(speed * moveTime * -1, rb.velocity.y);
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
}
