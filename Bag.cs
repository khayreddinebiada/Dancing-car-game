using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour {

    public GameObject TouchEffect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onTouch()
    {

        if (TouchEffect) Instantiate(TouchEffect, transform.position, transform.rotation).SetActive(true);
        if (AudioManager.audioManager) AudioManager.audioManager.StartTouchBag();
        Destroy(gameObject);
    }

}
