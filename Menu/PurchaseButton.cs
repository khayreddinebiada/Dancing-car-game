using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Menu
{
    public class PurchaseButton : MonoBehaviour
    {
        public int indexLevel = 3;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PurchaseSuccessfull(Product product)
        {
            Data.setUnlockLevel(indexLevel, true);
            ControlLevels.controlLevels.levels[indexLevel].isOpen = true;
            ControlUI.controlUI.ControlClickToPlay();
            ControlUI.controlUI.changeInfoData(indexLevel);
        }

    }
}