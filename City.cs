using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

    public Vector2 duringActive;
    public GameObject body;
	// Use this for initialization
	void Start () {

        body.SetActive(false);

        StartCoroutine(WaitingFuns.WaitAndSetActive(body, duringActive.x, true));
        StartCoroutine(WaitingFuns.WaitAndSetActive(body, duringActive.y, false));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
