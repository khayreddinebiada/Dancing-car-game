using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager audioManager;

    private static AudioSource audioMusic;

    private static AudioSource audioCollision;
    private static AudioSource audioTouchBag;
    private static AudioSource audioTouchMoney;
    private static AudioSource audioLoadMoney;
    private static AudioSource audioTap;
    private static AudioSource TakeScreenShot;

    private static bool isPlay = true;

    void Awake()
    {
        audioManager = GetComponent<AudioManager>();

    }

	// Use this for initialization
	void Start () 
    {

        audioMusic = GetComponents<AudioSource>()[0];
        audioCollision = GetComponents<AudioSource>()[1];
        audioTouchBag = GetComponents<AudioSource>()[2];
        audioTouchMoney = GetComponents<AudioSource>()[3];
        audioLoadMoney = GetComponents<AudioSource>()[4];

        isPlay = Data.GetPlayAudio();
        if (isPlay) audioMusic.Play();
        
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void VolumeMusic(float vol)
    {
        audioMusic.volume = vol;
    }

    public void StartMusic ()
    {
        if (audioMusic)
        {
            audioMusic.volume = 1f;
            if (isPlay) audioMusic.Play();
        }
    }

    public void StartCollision()
    {
        if (audioCollision && isPlay) audioCollision.Play();
    }

    public void StartTouchBag()
    {
        if (audioTouchBag && isPlay) audioTouchBag.Play();
    }

    public void StartTouchMoney()
    {
        if (audioTouchMoney && isPlay) audioTouchMoney.Play();
    }

    public void StartLoadMoney()
    {
        if (audioLoadMoney && isPlay) audioLoadMoney.Play();
    }

    public IEnumerator waitAndStop(float tm)
    {
        yield return new WaitForSeconds(tm);

        if (audioManager) audioManager.VolumeMusic(0.7f);
    }

    public static void playerTakeScreenShot()
    {

        if (TakeScreenShot && isPlay) TakeScreenShot.Play();

    }
}
