using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour {


    [SerializeField] private Queue<GameObject> tower;
    [SerializeField] private int maxCapacity = 5;
    [SerializeField] private Grid towerGrid;
    [SerializeField] private GameObject lastTower;
    [SerializeField] private GameObject towerPrf;

    private float timer = 0;
	// Use this for initialization
	void Start () {
        tower = new Queue<GameObject>();
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //FIXME: Do it every given distance.
        if (timer >= 7)
        {
            SpawnTower();
            timer = 0;
        }
    }
    void SpawnTower() {
        Vector3 pos = new Vector3(lastTower.transform.position.x, lastTower.transform.position.y + 4f, lastTower.transform.position.z);
        GameObject go = (GameObject)Instantiate(towerPrf, pos, Quaternion.identity);
        go.transform.SetParent(towerGrid.transform);
        tower.Enqueue(go);
        lastTower = go;

        Debug.Log(tower.Count);

        if (tower.Count >= maxCapacity)
        {
            Debug.Log(tower.Peek().name + " Eliminated new count is " + tower.Count);
            GameObject aux = tower.Dequeue();
            Destroy(aux);
        }
    }
}
