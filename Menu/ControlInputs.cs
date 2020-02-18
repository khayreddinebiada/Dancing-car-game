using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class ControlInputs : MonoBehaviour
    {

        public static float currentDistance = 0;

        private static Vector3 touchPosInit;
        // Use this for initialization
        void Start()
        {
            currentDistance = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static float GetMovingDestance(out bool isEndTouch)
        {

            isEndTouch = false;
#if UNITY_EDITOR

            if (Input.GetMouseButtonDown(0))
            {
                isEndTouch = false;
                touchPosInit = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {
                isEndTouch = false;
                return currentDistance = Vector3.Distance(touchPosInit, Input.mousePosition) * ((touchPosInit.x < Input.mousePosition.x) ? -1 : 1);

            }

            if (Input.GetMouseButtonUp(0))
            {
                isEndTouch = true;
                return currentDistance = Vector3.Distance(touchPosInit, Input.mousePosition) * ((touchPosInit.x < Input.mousePosition.x) ? -1 : 1);
            }

#elif UNITY_ANDROID

            if (0 < Input.touchCount)
            {
                foreach (Touch touch in Input.touches)
                {

                    if (touch.phase == TouchPhase.Began)
                    {
                        isEndTouch = false;
                        touchPosInit = touch.position;
                    }

                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        isEndTouch = false;
                        return currentDistance = Vector3.Distance(touchPosInit, touch.position) * ((touchPosInit.x < touch.position.x) ? -1 : 1);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        isEndTouch = true;
                        return currentDistance = Vector3.Distance(touchPosInit, Input.mousePosition) * ((touchPosInit.x < Input.mousePosition.x) ? -1 : 1);
                    }

                }
            }

#endif
            return currentDistance = 0;
        }

    }

}
