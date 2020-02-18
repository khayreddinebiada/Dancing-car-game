using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
    private int numberOfTotalLevels = 8;
    

    private static int playAudio;
    private static string dataPlayAudio;

    private static string dataStageNumber;
    private static int stageNumber;

    private static string dataNameTurnNumber;
    private static int turnNumber;

    private static string dataNameBagsNumber;
    private static int bagsNumber;

    private static string dataNamePercent;
    private static int percent;

    public static int numberOfLevelWin = 0;
    public static int totalBags = 0;

    private static string dataLevelIsLocked;

    private static string addCommentForMyGame;

    private ControlGame controlGame;

	// Use this for initialization
    void Awake()
    {
        //PlayerPrefs.DeleteAll();

        controlGame = GetComponent<ControlGame>();
        dataLevelIsLocked = "Level Locked";
        addCommentForMyGame = "Add Comment";
        dataNameBagsNumber = "BagsNumber" + controlGame.numberOfLevel;
        dataNameTurnNumber = "TurnNumber" + controlGame.numberOfLevel;
        dataNamePercent = "Percent" + controlGame.numberOfLevel;

        dataPlayAudio = "Audio Is Play";

        if (!PlayerPrefs.HasKey(dataPlayAudio))
            PlayerPrefs.SetInt(dataPlayAudio, playAudio = 1);
        else
            playAudio = PlayerPrefs.GetInt(dataPlayAudio);

        stageNumber = PlayerPrefs.GetInt(dataStageNumber);
        turnNumber = PlayerPrefs.GetInt(dataNameTurnNumber);
        bagsNumber = PlayerPrefs.GetInt(dataNameBagsNumber);
        percent = PlayerPrefs.GetInt(dataNamePercent);


        // getNumber of Stages win and Number of bags
        int bgs = 0;
        int tLevelWin = 0;
        
        for (int i = 1; i <= numberOfTotalLevels; i++)
        {

            bgs += PlayerPrefs.GetInt("BagsNumber" + i);

            if (PlayerPrefs.GetInt("Percent" + i) == 100)
                tLevelWin ++;

        }

        numberOfLevelWin = tLevelWin;
        totalBags = bgs;

	}

    public static bool GetCommentStatusIsShowed()
    {
        return PlayerPrefs.HasKey(addCommentForMyGame);
    }

    public static void CommentIsDisplayed()
    {
        PlayerPrefs.SetInt(addCommentForMyGame, 1);

    }

	// Update is called once per frame
	void Update () {
		
	}

    public static bool showHowToPlay()
    {
        return (PlayerPrefs.GetInt("Show How To Play") == 1) ? true : false;
    }

    public static void HowToPlayIsSet()
    {
        PlayerPrefs.SetInt("Show How To Play", 1);
    }

    public static void setUnlockLevel(int LevelId, bool Unlock)
    {
        PlayerPrefs.SetInt(dataLevelIsLocked + LevelId.ToString(), (Unlock)? 1 : 0);
    }

    public static bool GetUnlockLevel(int LevelId)
    {
        return (PlayerPrefs.GetInt(dataLevelIsLocked + LevelId.ToString()) == 1) ? true : false;
    }

    public static void InverseMusic()
    {
        playAudio = (playAudio == 0)? 1 : 0;
        PlayerPrefs.SetInt(dataPlayAudio, playAudio);
    }

    public static void SetPlayAudio( bool isPlay)
    {
        playAudio = (isPlay)? 1 : 0;
        PlayerPrefs.SetInt(dataPlayAudio, playAudio);
    }

    public static bool GetPlayAudio()
    {
        return (playAudio == 1) ? true : false;
    }

    public static int getStageNumber()
    {

        return stageNumber;

    }

    public static int getStageNumberWithPrefabs()
    {

        return stageNumber = PlayerPrefs.GetInt(dataStageNumber);

    }

    public static void setStageNumber(int stageNumber)
    {
        PlayerPrefs.SetInt(dataStageNumber, Data.stageNumber = stageNumber);
    }


    public static int getBagsNumber()
    {
        return bagsNumber;
    }

    public static int getBagsNumber(int level)
    {
        return PlayerPrefs.GetInt("BagsNumber"+level);
    }

    public static void changeBagsNumber(int bagsNb)
    {
        bagsNumber = Mathf.Max(bagsNumber, bagsNb);

        PlayerPrefs.SetInt(dataNameBagsNumber, bagsNumber);

    }

    public static int getPercent()
    {
        return percent;
    }

    public static int getPercent(int level)
    {
        return PlayerPrefs.GetInt("Percent" + level);
    }

    public static void changePercent(int prc)
    {
        percent = Mathf.Max(percent, prc);

        PlayerPrefs.SetInt(dataNamePercent, percent);

    }

    public static int getTurnNumber()
    {

        return turnNumber;

    }

    public static int getTurnNumber(int level)
    {
        return PlayerPrefs.GetInt("TurnNumber" + level);
    }

    public static void changeTurnNumber(int turnNb)
    {
        turnNumber = Mathf.Max(turnNumber, turnNb);

        PlayerPrefs.SetInt(dataNameTurnNumber, turnNumber);

    }

}
