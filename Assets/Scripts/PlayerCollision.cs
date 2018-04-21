using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollision : MonoBehaviour {
    
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            collision.gameObject.GetComponent<TilemapCollider2D>().isTrigger = false;
        }
    }
}
