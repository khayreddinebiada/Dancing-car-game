using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ControlUI : MonoBehaviour {

    [Header("On Play")]

    public Slider percentOnPlay;
    public Slider maxPercent;
    public Text nbTurnTextOnPlay;
    public Text begsTextOnPlay;

    [Header("Lost")]

    public Text percentText;
    public Text maxPercentLossPanel;
    public Slider percentLoss;
    public Text begsText;
    public Text nbTurnText;

    //public GameObject screenshotPreview;
    //public GameObject EffectCameraFlash;

    [Header("Panels")]
    public GameObject PanelOnPlay;
    public GameObject UIOnTap;
    public GameObject PanelLoss;
    public GameObject LoadingPanel;


    [Header("Win")]
    public GameObject WinPanel;
    public Text percentWinText;
    public Text begsTextWin;
    public Text nbTurnTextWin;
    public Slider percentWin;
    public GameObject RatePanel;


    public static ControlUI controlUI;

    private Player player;
    private bool isLoss = false;

    void Awake()
    {
        controlUI = this;

    }

	// Use this for initialization
	void Start () {

        StartCoroutine(recursivePercent(maxPercent, null, Data.getPercent(), false));

        isLoss = false;

        player = Player.player;

        if (RatePanel != null && Data.GetCommentStatusIsShowed())
        {
            RatePanel.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.main.transform.parent = transform.parent;
            Player.player.gameObject.SetActive(false);

            Time.timeScale = 1;
            LoadingPanel.SetActive(true);
            SceneManager.LoadSceneAsync("Menu");

        }

        if (Player.player.isLoss && !isLoss)
        {


            StartCoroutine(waitAndStartCourotine());

            begsText.text = player.nbBags.ToString();


            Data.changeBagsNumber(player.nbBags);

            Data.changePercent((int)player.gamePercent);


            isLoss = true;

        }

	}

    public void SetRateTheGame()
    {
        Data.CommentIsDisplayed();
    }

    public void IsWin()
    {
        StartCoroutine(recursivePercent(percentWin, percentWinText, 100, true));
    }

    public IEnumerator waitAndStartCourotine()
    {

        yield return new WaitForSeconds(1);

        StartCoroutine(recursivePercent(percentLoss, percentText, (int)player.gamePercent, true));

    }

    public IEnumerator recursivePercent(Slider slider, Text textSlider, int max, bool isAudioActive)
    {

        yield return new WaitForSeconds(0.03f);

        if (slider.value < max)
        {
            slider.value++;

            if (isAudioActive)
                AudioManager.audioManager.StartLoadMoney();
            if (textSlider)
                textSlider.text = "% " + slider.value.ToString();

            StartCoroutine(recursivePercent(slider, textSlider, max, isAudioActive));
        }

    }

    IEnumerator recursiveAddMoneys(Text textAdded, Text textRemoved, int amoutAdd, int from, float timeAdd)
    {

        yield return new WaitForSeconds(timeAdd);

        from++;
        amoutAdd--;

        if (from <= from + amoutAdd)
        {

            textAdded.text = "$" + from.ToString();

            AudioManager.audioManager.StartLoadMoney();

            StartCoroutine(recursiveAddMoneys(textAdded, textRemoved, amoutAdd, from, Mathf.Clamp(timeAdd - 0.02f, 0.03f, timeAdd)));

        }


    }

    public void Replay()
    {

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

    }

//    public void takeScreenshot(GameObject imageReview)
//    {

//        string pathImage = "Screen " + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
//        ScreenCapture.CaptureScreenshot(pathImage);

//        screenshotPreview.SetActive(false);

//        StartCoroutine(waitAndDeactive(pathImage));
//        StartCoroutine(waitAndFlash());
//        StartCoroutine(waitAndSetActive(imageReview));
//    }

//    IEnumerator waitAndFlash()
//    {

//        yield return new WaitForSeconds(0.5f);

//        EffectCameraFlash.SetActive(true);
//        AudioManager.playerTakeScreenShot();

//    }

//    IEnumerator waitAndDeactive(string pathImg)
//    {

//        yield return new WaitForSeconds(1f);

//#if UNITY_EDITOR
//        // Read the data from the file
//        byte[] data = File.ReadAllBytes(pathImg);
//#else
//        // Read the data from the file
//        byte[] data = File.ReadAllBytes(Application.persistentDataPath + "/" + pathImg);
//#endif

//        // Create the texture
//        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height);

//        // Load the image
//        screenshotTexture.LoadImage(data);

//        // Create a sprite
//        Sprite screenshotSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));

//        // Set the sprite to the screenshotPreview
        
//        screenshotPreview.SetActive(true);
//        // screenshotPreview.GetComponent<Animator>().Play("Move mobile");
//        screenshotPreview.GetComponent<Image>().sprite = screenshotSprite;

//        // StartCoroutine(waitAndPlayAnim(screenshotPreview));

//    }

//    IEnumerator waitAndPlayAnim(GameObject obj)
//    {

//        yield return new WaitForSeconds(2f);

//        screenshotPreview.SetActive(false);
//        EffectCameraFlash.SetActive(false);

//    }

    IEnumerator waitAndSetActive(GameObject obj)
    {

        yield return new WaitForSeconds(1f);

        obj.SetActive(true);

    }

    public void gotToLink(string link)
    {
        Application.OpenURL(link);
    }

    public void IsStop(bool replayAudio)
    {
        Player.player.isStop = false;
        UIOnTap.SetActive(false);
        Time.timeScale = Player.player.timeScale;
        if (AudioManager.audioManager && replayAudio) AudioManager.audioManager.StartMusic();
    }

    public void SetAllowInput(bool allow)
    {
        ControllerInputs.allowInputs = allow;
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

}
