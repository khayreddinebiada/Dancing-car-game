using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingFuns : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public static IEnumerator WaitAndActiveAnimation(Animator animator, float TimeWaiting)
    {
        yield return new WaitForSeconds(TimeWaiting);
        if (animator) animator.enabled = true;
    }

    public static IEnumerator WaitAndDestroy(Object Object, float TimeWaiting)
    {
        yield return new WaitForSeconds(TimeWaiting);
        Destroy(Object);
    }

    public static IEnumerator WaitAndSetActive(GameObject Object, float TimeWaiting, bool value)
    {
        yield return new WaitForSeconds(TimeWaiting);
        Object.SetActive(value);
    }

}
