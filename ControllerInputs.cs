using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputs : MonoBehaviour {

    public static bool allowInputs = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        controllerMove();
	}

    void controllerMove()
    {
        if (allowInputs)
        {

#if UNITY_EDITOR

            if (Input.GetMouseButtonDown(0))
            {
                Player.player.DirectionChanged();
            }

#elif UNITY_ANDROID
        
        if (0 < Input.touchCount)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Player.player.DirectionChanged();
            }
        }

#endif

        }
    }
}
