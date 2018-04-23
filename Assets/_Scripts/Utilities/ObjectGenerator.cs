using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {


    [SerializeField] private Queue<GameObject> listOfObjects;
    [SerializeField] private int maxCapacity = 5;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject lastObjectGenerated;
    [SerializeField] private GameObject objectPrefab;
    public float delayBetweenGenerations = 6;
    public float distanceOfGeneration = 4;

    [SerializeField] private GameObject specialPrefab;
    public float specialPrefabGenerationHeight;
    bool specialPrefabPlaced = false;

    [SerializeField] private GameObject nextPrefab;
    public float newDistanceOfGeneration;
    public float newDelayOfGenerations;
    [SerializeField] GameObject[] clouds;
    private int randomIndex;

    public bool mountainZone;
    public bool skyZone;
    public bool spaceZone;

    private float timer = 0;
	// Use this for initialization
	void Start () {
        listOfObjects = new Queue<GameObject>();
        mountainZone = true;
        skyZone = false;
        spaceZone = false;
	}

    // Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    //FIXME: Do it every given distance.
    //    if (timer >= delayBetweenGenerations)
    //    {
    //        SpawnTower();
    //        timer = 0;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TowerBG" && specialPrefab == null) {
            SpawnTower();
        }
        if (collision.gameObject.tag == "BackGround" && specialPrefab != null)
        {
            SpawnTower();
        }
    }
    void SpawnTower() {
        Vector3 pos = new Vector3(lastObjectGenerated.transform.position.x, lastObjectGenerated.transform.position.y + distanceOfGeneration, lastObjectGenerated.transform.position.z);
        GameObject go;
        if (specialPrefab != null && pos.y >= specialPrefabGenerationHeight && specialPrefabPlaced == false)
        {
            go = (GameObject)Instantiate(specialPrefab, pos, Quaternion.identity);
            generateClouds(go);
            specialPrefabPlaced = true;
            if (skyZone == false)
            {
                mountainZone = false;
                skyZone = true;
                spaceZone = false;
            }
        }
        else if (specialPrefabPlaced == true)
        {
            pos = new Vector3(lastObjectGenerated.transform.position.x, lastObjectGenerated.transform.position.y + newDistanceOfGeneration, lastObjectGenerated.transform.position.z);
            go = (GameObject)Instantiate(nextPrefab, pos, Quaternion.identity);
            generateClouds(go);
            delayBetweenGenerations = newDelayOfGenerations;
        }
        else {
            if (mountainZone == false) {
                mountainZone = true;
                skyZone = false;
                spaceZone = false;
            }
            go = (GameObject)Instantiate(objectPrefab, pos, Quaternion.identity);
            delayBetweenGenerations = newDelayOfGenerations;

        }
        go.transform.SetParent(grid.transform);
        listOfObjects.Enqueue(go);
        lastObjectGenerated = go;

        Debug.Log(listOfObjects.Count);

        if (listOfObjects.Count >= maxCapacity)
        {
            Debug.Log(listOfObjects.Peek().name + " Eliminated new count is " + listOfObjects.Count);
            GameObject aux = listOfObjects.Dequeue();
            Destroy(aux);
        }
    }
    void generateClouds(GameObject go) {
        int numOfClouds = GenerateRandomInt(3, 10);
        for (int i = 0; i < numOfClouds; i++)
        {
            int randomIndex = GenerateRandomInt(0, clouds.Length);
            Vector3 cloudPos = new Vector3(-12, go.transform.position.y + GenerateRandomInt(-40, 0), go.transform.position.y);
            GameObject cloud = (GameObject)Instantiate(clouds[randomIndex], cloudPos, Quaternion.identity);
            cloud.transform.SetParent(go.transform);
        }
    }
    int GenerateRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }
}
