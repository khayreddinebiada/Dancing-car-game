using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour {

    public GameObject tapToScreen;

    public float[] TimesStopGame;
    private bool[] isStoped;
	// Use this for initialization
	void Start () {

        isStoped = new bool[TimesStopGame.Length];

        for (int i = 0; i < isStoped.Length; i++)
            isStoped[i] = false;

	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < TimesStopGame.Length; i++)
        {
            if (!isStoped[i] && TimesStopGame[i] < Player.player.time)
            {
                SetStop();
                isStoped[i] = true;
            }
        }

	}

    public void SetStop()
    {
        Time.timeScale = 0;
        ControllerInputs.allowInputs = true;
        tapToScreen.SetActive(true);
    }

    public void SetStart()
    {
        Time.timeScale = 1;
        ControllerInputs.allowInputs = false;
        tapToScreen.SetActive(false);
    }

    public void SetAllowInput(bool allow)
    {
        ControllerInputs.allowInputs = allow;
    }
}
