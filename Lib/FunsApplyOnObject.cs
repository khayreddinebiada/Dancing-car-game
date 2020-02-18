using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunsApplyOnObject : MonoBehaviour {
    [Header("Set Active Object by time")]
    public bool UseSetActive = false;
    public float SetActiveObjectOnTime = 20;
    public float SetDeactiveObjectOnTime = 50;
    public GameObject bodyDisplay;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (UseSetActive)
        {
            if (SetActiveObjectOnTime < Player.player.time && Player.player.time < SetDeactiveObjectOnTime)
            {
                bodyDisplay.SetActive(true);
            }
            else
            {
                bodyDisplay.SetActive(false);
            }
        }

	}
}
