using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

    // Use this for initialization

    private Vector3 direction;
    private float speed;
    public float minSpeed;
    public float maxSpeed;
    public float secondsToDestroy = 5;
	void Start () {
        speed = Random.Range(minSpeed, maxSpeed) / 200;
        direction = new Vector3(1, 0, 0);

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed);
        Destroy(this.gameObject, secondsToDestroy);
	}
}
