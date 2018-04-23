using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    #region Variable
    public GameObject[] platforms;
    [SerializeField] private int platformIndex;
    [SerializeField] private GameObject tileGrid;
    [SerializeField] private Transform nextPosition;
    [SerializeField] private Queue<GameObject> platformPool;
    [SerializeField] private int maxCapacity = 3;

    float timer = 0f;
    #endregion
    private void Start()
    {
        platformPool = new Queue<GameObject>();

    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        //FIXME: Do it every given distance.
        if (timer >= 8){
            SpawnPlatform();
            timer = 0;
        }
    }


    #region Custom Functions
    void SpawnPlatform() {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 8.2f, transform.position.z);
        GameObject go = (GameObject)Instantiate(platforms[platformIndex], pos, Quaternion.identity);
        go.transform.SetParent(tileGrid.transform);
        platformPool.Enqueue(go);

        Debug.Log(platformPool.Count);

        if (platformPool.Count >= maxCapacity) {
            Debug.Log(platformPool.Peek().name + " Eliminated new count is " + platformPool.Count);
            GameObject aux = platformPool.Dequeue();
            Destroy(aux);
        }

        //platformPool.Push(go);
        
    }
    void RandomIndex() {
        platformIndex = Random.Range(0, platforms.Length);
    }



    #endregion
}
