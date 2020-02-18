using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoading : MonoBehaviour {

    static DontDestroyOnLoading objectDontDestroyOnLoading;

	// Use this for initialization
	void Start () {
        if (objectDontDestroyOnLoading != null)
        {
            Destroy(gameObject);

        }
        else
        {
            objectDontDestroyOnLoading = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
