using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;

namespace Menu
{

    public class ControlUI : MonoBehaviour
    {
        public Text percentText;
        public Slider percentSlider;
        public Text bagsText;
        public Text turnsText;
        public Text StageType;

        public Image musicImage;


        public Image lockIcon;
        public Image comingSoon;
        public Button buttonTapToStart;

        public Sprite OnMusic;
        public Sprite OffMusic;

        public Text textStatLevel;
        public Text textUnlock;

        public GameObject ExitPanel;
        public AudioSource clickFault;
        public Animator lockPanel;

        public PurchaseButton[] pButtons;
        public static ControlUI controlUI;
        

        private static int maxPercent;
        
        // Use this for initialization
        void Start()
        {

            controlUI = this;

            if (Data.GetPlayAudio())
            {
                musicImage.sprite = OnMusic;
            }
            else
            {
                musicImage.sprite = OffMusic;
            }
            
            ControlClickToPlay();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ExitPanel.SetActive(true);
            }

            ControlClickToPlay();
        }

        public void ErrorPurchase(Product product, PurchaseFailureReason reason)
        {
            textUnlock.text = reason.ToString();
        }

        public void ControlClickToPlay()
        {
            Level level = ControlLevels.controlLevels.levels[ControlLevels.controlLevels.currentLevelId];

            if (level.isCoomingSoon)
            {
                textStatLevel.text = "IS CLOSE";
                lockIcon.gameObject.SetActive(false);
                comingSoon.gameObject.SetActive(true);
            }
            else
            {
                if (level.isOpen)
                {
                    textStatLevel.text = "TAP TO START";
                    lockIcon.gameObject.SetActive(false);
                    comingSoon.gameObject.SetActive(false);
                }
                else
                {
                    textStatLevel.text = "IS CLOSE";
                    lockIcon.gameObject.SetActive(true);
                    comingSoon.gameObject.SetActive(false);
                }
            }

        }

        public void changeInfoData(int levelNumber)
        {

            if (!ControlLevels.controlLevels.levels[levelNumber].isOpen)
            {

                int win = ControlLevels.controlLevels.levels[levelNumber].nbLevelWinToUnlock;
                // int bgs = ControlLevels.controlLevels.levels[levelNumber].nbBagsToUnlock;
                if (0 < win) textUnlock.text = "WIN " + ControlLevels.controlLevels.levels[levelNumber].nbLevelWinToUnlock + " LEVELS TO UNLOCK THIS LEVEL";
                textUnlock.gameObject.SetActive(true);

            }
            else
            {
                textUnlock.gameObject.SetActive(false);
            }

            if (ControlLevels.controlLevels.levels[levelNumber].isCoomingSoon)
                textUnlock.gameObject.SetActive(false);

            maxPercent = Data.getPercent(levelNumber + 1);
            int bags = Data.getBagsNumber(levelNumber + 1);
            int turns = Data.getTurnNumber(levelNumber + 1);



            StageType.text = ControlLevels.controlLevels.levels[levelNumber].difficulty;

            bagsText.text = bags.ToString();
            turnsText.text = turns.ToString();

            percentText.text = "% 0";
            percentSlider.value = 0;

            for (int i = 0; i < pButtons.Length; i++)
            {
                if (pButtons[i] != null)
                {
                    if (levelNumber != i)
                    {
                        pButtons[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        pButtons[i].gameObject.SetActive(true);
                    }
                }
            }

            StartCoroutine(recursivePercent(percentSlider, percentText));

        }

        public void ExitFromGame(bool isExit)
        {
            if (isExit)
            {
                Application.Quit();
            }
            else
            {
                ExitPanel.SetActive(false);
            }
        }

        public IEnumerator recursivePercent(Slider slider, Text textSlider)
        {

            yield return new WaitForSeconds(0.02f);

            if (slider.value < maxPercent)
            {
                slider.value = Mathf.Clamp(slider.value + 3, 0, maxPercent);

                if (textSlider)
                    textSlider.text = "% " + slider.value.ToString();

                StartCoroutine(recursivePercent(slider, textSlider));
            }

        }

        public void GoToScene()
        {

            float distanceMax = 5 * Screen.width / 800;

            if (ControlInputs.currentDistance <= distanceMax && ControlInputs.currentDistance >= -distanceMax)
            {
                if (ControlLevels.controlLevels.levels[ControlLevels.controlLevels.currentLevelId].isOpen)
                {

                    if (!Data.showHowToPlay())
                    {
                        SceneManager.LoadSceneAsync("Learning");
                        Data.HowToPlayIsSet();
                    }
                    else
                    {
                        SceneManager.LoadSceneAsync(ControlLevels.controlLevels.levels[ControlLevels.controlLevels.currentLevelId].name);
                        ControlCamera.LevelIsSelected();

                        buttonTapToStart.enabled = false;
                    }

                }
                else
                {

                    lockPanel.SetTrigger("SetFault");

                    if (Data.GetPlayAudio())
                        clickFault.Play();

                }
            }

        }

        public void WatchVideo()
        {

            bool videoIsPlay = AdsManager.showAdRewardedVideo();
            if (!videoIsPlay)
            {
                // is not we can't entry to this level.
                if (Data.GetPlayAudio())
                    clickFault.Play();

                textUnlock.text = "VIDEO NOT AVAILABLE PLEASE CHECK YOUR CONNECTION AND TRY AGAIN.";

                lockPanel.SetTrigger("SetFault");
            }
            else
            {

                SceneManager.LoadSceneAsync(ControlLevels.controlLevels.levels[ControlLevels.controlLevels.currentLevelId].name);
                ControlCamera.LevelIsSelected();

                textUnlock.text = "Waiting...";
                buttonTapToStart.enabled = false;

            }

        }

        public void ClickOnOfMusic()
        {
            Data.InverseMusic();

            if (Data.GetPlayAudio())
            {
                musicImage.sprite = OnMusic;
            }
            else
            {
                musicImage.sprite = OffMusic;
            }

        }
        
        public void gotToLink(string link)
        {
            Application.OpenURL(link);
        }

    }

}
