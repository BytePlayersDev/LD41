using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPlayer : MonoBehaviour {

    public PlayerController pc;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            pc.SetIsJumping(false);
            pc.SetIsMoving(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if(pc.GetIsMoving())
            pc.SetIsJumping(true);
            pc.SetIsMoving(true);
        }
    }
}
