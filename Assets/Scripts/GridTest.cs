using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTest : MonoBehaviour {

    public GameObject prefab; // Prefab

    public int numberToCreate; // Numero de elementos a crear

	// Use this for initialization
	void Start ()
    {
        Populate();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void Populate()
    {
        GameObject newObj;

        for (int i = 0; i < numberToCreate; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform); // Crea una nueva instancia del prefab que hemos especificado
            newObj.GetComponent<Image>().color = Random.ColorHSV(); // Randomiza el color del componente imagen del anterior prefab
        }
    }
}
