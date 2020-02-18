using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour {
    public enum LightingType { Police };

    public LightingType lightingType = LightingType.Police;
    public Light LightPoliceRight;
    public Light LightPoliceLeft;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (LightingType.Police == lightingType)
        {

            int a = Random.Range(0, 2);

            if (a == 1)
            {
                LightPoliceLeft.enabled = true;
                LightPoliceRight.enabled = false;
            }
            else
            {
                LightPoliceLeft.enabled = false;
                LightPoliceRight.enabled = true;
            }

        }

	}
}
