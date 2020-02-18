
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGame : MonoBehaviour {

    public GameObject[] gameObjectLoading;

    public int numberOfLevel = 0;
    public bool isMenu = false;
    public static ControlGame controlGame;

	// Use this for initialization
	void Awake ()
    {

        for (int i = 0; i < gameObjectLoading.Length; i++)
        {
            gameObjectLoading[i] = Instantiate(gameObjectLoading[i]);
        }


        controlGame = this;
	}

    void start()
    {
        if (isMenu)
        {
            numberOfLevel = Data.getStageNumber();
        }
    }

	// Update is called once per frame
	void Update () {
	}

}
