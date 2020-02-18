using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour {

    public float speedMoveWinCamera = 1;
    public float speedLossVibrate = 10f;
    public GameObject EffectLoss;
    private bool gameIsFinish = false;
    private bool isLoss = false;

    private int Derection = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Player.player.isLoss || Player.player.isWin && !gameIsFinish)
        {
            transform.parent = Player.player.transform.parent;
            gameIsFinish = true;

            if (EffectLoss) StartCoroutine(WaitAndSetActive(EffectLoss, 2, true));

            if (Player.player.isLoss && !isLoss)
            {
                isLoss = true;
                StartCoroutine(waitAndchangeDerection(0.1f));
            }

        }

        if (gameIsFinish)
        {
            transform.position += speedMoveWinCamera * Vector3.up * Time.deltaTime;
        }

        IsLoss();
	}

    void IsLoss()
    {
        
        if (isLoss)
        {
            transform.position += speedLossVibrate * transform.right * Time.deltaTime * Derection;
            speedLossVibrate = Mathf.Clamp(speedLossVibrate - 1f, 0, speedLossVibrate);
        }

    }

    public IEnumerator WaitAndSetActive(GameObject Object, float TimeWaiting, bool value)
    {
        yield return new WaitForSeconds(TimeWaiting);
        Object.SetActive(value);
    }

    IEnumerator waitAndchangeDerection(float wait)
    {
        yield return new WaitForSeconds(wait);

        Derection = (Derection == -1) ? 1 : -1;

        StartCoroutine(waitAndchangeDerection(0.1f));
    }

}
