using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    #region Variable
    [SerializeField] private Queue<GameObject> platformPool;
    [SerializeField] private int maxCapacity = 3;
    [SerializeField] private Grid tileGrid;
    [SerializeField] private GameObject lastPlatform;
    public GameObject[] platformsPrfs;
    [SerializeField] private int platformIndex;
    public float delayBetweenGenerations = 6;
    public float distanceOfGeneration = 5f;

    float timer = 0f;
    #endregion
    private void Start()
    {
        platformPool = new Queue<GameObject>();

    }
    private void FixedUpdate()
    {
        //timer += Time.deltaTime;
        ////FIXME: Do it every given distance.
        //if (timer >= delayBetweenGenerations)
        //{
        //    SpawnPlatform();
        //    timer = 0;
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Waypoint") {
            SpawnPlatform();
        }
    }


    #region Custom Functions
    void SpawnPlatform() {
        Vector3 pos = new Vector3(lastPlatform.transform.position.x, lastPlatform.transform.position.y + distanceOfGeneration, lastPlatform.transform.position.z);
        RandomIndex();
        GameObject go = (GameObject)Instantiate(platformsPrfs[platformIndex], pos, Quaternion.identity);
        go.transform.SetParent(tileGrid.transform);
        platformPool.Enqueue(go);
        lastPlatform = go;

        Debug.Log(platformPool.Count);

        if (platformPool.Count >= maxCapacity) {
            Debug.Log(platformPool.Peek().name + " Eliminated new count is " + platformPool.Count);
            GameObject aux = platformPool.Dequeue();
            Destroy(aux);
        }
        
    }
    void RandomIndex() {
        platformIndex = Random.Range(0, platformsPrfs.Length);
    }



    #endregion
}
