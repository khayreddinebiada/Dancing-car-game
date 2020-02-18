using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {



    public GameObject Body;
    public int numberOfMove = 0;
    public float deltaMoveTime = 0.4f;
    public float timeAdded = 0;
    public float speed = 10;
    public bool isMove = true;
    
    private float timeToMove = 10;
	// Use this for initialization
    void Start()
    {
        timeToMove = deltaMoveTime * numberOfMove + timeAdded;

	}
	
	// Update is called once per frame
    void Update()
    {
        if (timeToMove <= Player.player.time && isMove)
        {
            Body.transform.localPosition += Vector3.forward * Time.deltaTime * speed;
        }
	}


}
