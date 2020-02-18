using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{

    public class ControlCamera : MonoBehaviour
    {
        public float distanceMinToMovingNext = 25;
        public float speedMoving = 10;
        public float speedMovingToCar = 3;
        private ControlLevels controlLevels;
        private bool isMoved = false;
        private static bool isSelectLevel = false;

        // Use this for initialization
        void Start()
        {
            
            controlLevels = GetComponent<ControlLevels>();
            transform.position = controlLevels.levels[controlLevels.currentLevelId].pShowEnvirenement.position;
            isSelectLevel = false;

        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (!isSelectLevel)
            {
                if (MovingCamera())
                {
                    GoToPosition(controlLevels.currentLevelId);
                }
            }
            else
            {
                SelectLevel();
            }

        }

        public static void LevelIsSelected()
        {
            isSelectLevel = true;
        }

        public void SelectLevel()
        {
            if (!controlLevels.levels[controlLevels.currentLevelId].isCoomingSoon)
            {
                transform.position = Vector3.MoveTowards(transform.position, controlLevels.levels[controlLevels.currentLevelId].pShowCar.position, speedMovingToCar * Time.fixedDeltaTime);
            }
        }
        
        bool MovingCamera()
        {
            
            bool isEndTouch = false;
            float distance = ControlInputs.GetMovingDestance(out isEndTouch);

            if (!isEndTouch)
            {

                if (0 < distance && controlLevels.currentLevelId < controlLevels.levels.Length - 1)
                {

                    Vector3 posCurrent = controlLevels.levels[controlLevels.currentLevelId].pShowEnvirenement.position;
                    Vector3 posNext = controlLevels.levels[controlLevels.currentLevelId + 1].pShowEnvirenement.position;

                    transform.position = posCurrent + (posNext - posCurrent) * distance / Screen.width;

                    return false;
                }
                else
                    if (0 > distance && 0 < controlLevels.currentLevelId)
                    {
                        Vector3 posCurrent = controlLevels.levels[controlLevels.currentLevelId].pShowEnvirenement.position;
                        Vector3 posPrev = controlLevels.levels[controlLevels.currentLevelId - 1].pShowEnvirenement.position;

                        transform.position = posCurrent - (posPrev - posCurrent) * distance / Screen.width;

                        return false;
                    }

                isMoved = false;

            }
            else
            {

                if (distanceMinToMovingNext * Screen.width / 100 < distance && controlLevels.currentLevelId < controlLevels.levels.Length - 1 && !isMoved)
                {
                    controlLevels.changeStageToNext();
                    isMoved = true;
                }
                else
                    if (distance < -distanceMinToMovingNext * Screen.width / 100 && 0 < controlLevels.currentLevelId && !isMoved)
                    {
                        controlLevels.changeStageToPrev();
                        isMoved = true;
                    }
                
            }
            return true;

        }

        void GoToPosition(int idLevel)
        {
            transform.position = Vector3.MoveTowards(transform.position, controlLevels.levels[controlLevels.currentLevelId].pShowEnvirenement.position, speedMoving * Time.fixedDeltaTime);
        }

    }

}
