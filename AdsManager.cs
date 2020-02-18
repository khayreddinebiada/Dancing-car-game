using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
    private string gameID = "2974936";
    public static bool allowAds = true;

	// Use this for initialization
	void Awake () {
        Advertisement.Initialize (gameID, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public static bool showAdVideo()
    {
        if (allowAds)
        {
            
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
                return true;
            }

        }
        return false;
    }

    public static bool showAdRewardedVideo()
    {
        if (allowAds)
        {
            
            if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo");
                return true;
            }

        }
        return false;
    }
    
}
