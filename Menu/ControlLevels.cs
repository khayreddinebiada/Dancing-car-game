using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    [System.Serializable]
    public class Level
    {
        public int id_Level = -1;
        public string name;
        public Transform pShowEnvirenement;
        public Transform pShowCar;
        public string difficulty;
        public Color lightingColor;
        public bool isCoomingSoon = false;
        public bool isOpen = true;
        public int nbLevelWinToUnlock = 0;
        public int nbBagsToUnlock = 0;
    }


    public class ControlLevels : MonoBehaviour
    {
        public static ControlLevels controlLevels;
        public AudioSource[] Audios;
        public int currentLevelId = 0;

        public Level[] levels;
        
        void Awake()
        {
            controlLevels = GetComponent<ControlLevels>();
        }

        void OnEnable()
        {
            currentLevelId = Data.getStageNumberWithPrefabs();

        }

        // Use this for initialization
        void Start()
        {

            for (int i = 0; i < levels.Length; i++)
            {

                if (!levels[i].isOpen && !levels[i].isCoomingSoon)
                {

                    bool isOpen = Data.GetUnlockLevel(i);

                    if (isOpen)
                    {
                        levels[i].isOpen = true;
                    }
                    else
                    {
                        if (levels[i].nbLevelWinToUnlock <= Data.numberOfLevelWin)
                        {
                            Data.setUnlockLevel(i, true);
                            levels[i].isOpen = true;
                        }
                    }

                }
                else
                {
                    if (levels[i].isCoomingSoon)
                    {
                        Data.setUnlockLevel(i, false);
                        levels[i].isOpen = false;
                    }
                }
            }

            RenderSettings.fogColor = levels[currentLevelId].lightingColor;
            ControlUI.controlUI.changeInfoData(currentLevelId);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void changeStageToNext()
        {

            if (currentLevelId < levels.Length - 1)
            {
                currentLevelId++;
                Data.setStageNumber(currentLevelId);
                RenderSettings.fogColor = levels[currentLevelId].lightingColor;
                ControlUI.controlUI.changeInfoData(controlLevels.currentLevelId);
            }
        }

        public void changeStageToPrev()
        {
            if (0 < currentLevelId)
            {
                currentLevelId--;
                Data.setStageNumber(currentLevelId);
                RenderSettings.fogColor = levels[currentLevelId].lightingColor;
                ControlUI.controlUI.changeInfoData(controlLevels.currentLevelId);
            }

        }
        
    }

}
