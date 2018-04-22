﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    #region Variable
    public GameObject[] platforms;
    [SerializeField] private int platformIndex;
    [SerializeField] private GameObject tileGrid;
    [SerializeField] private Transform nextPosition;

    float timer = 0f;
    #endregion
    private void Update()
    {
        timer += Time.deltaTime;
        //FIXME: Do it every given distance.
        if (timer >= 4){
            SpawnPlatform();
            timer = 0;
        }
    }


    #region Custom Functions
    void SpawnPlatform() {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 8.2f, transform.position.z);
        GameObject go = (GameObject)Instantiate(platforms[platformIndex], pos, Quaternion.identity);
        go.transform.SetParent(tileGrid.transform);
    }
    void RandomIndex() {
        platformIndex = Random.Range(0, platforms.Length);
    }



    #endregion
}
