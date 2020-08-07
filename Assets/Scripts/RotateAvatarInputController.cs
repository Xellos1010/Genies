using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAvatarInputController : MonoBehaviour
{
    public float speed = 0.1f;
    private bool isTouchEnabled;
    Touch cachedTouch;
    public Vector3 initialPositionCache;
    public RotationController rotationController;
    public bool isInitialPositionSet = false;
    // Update is called once per frame
    void Update()
    {
        //Check if Initial Position is Set
        //No
        //Set Start Touch Position and wait until next
        //Yes
        //Calculate Difference in new position and previous position
        //Add Difference to rotation
        //Expansion - Seperate Rotation object and touch calculation into seperate scripts
        //Expansion - Add maximum Rotation per frame value - add canRotate Bool - OnTouchEnd Rotate to Finish - On Touch Start Reset amount to rotate by to 0
        if (!isInitialPositionSet)
        {
            //Check if Mouse or Touch is Down
#if UNITY_EDITOR
            if(Input.GetMouseButtonDown(0))
                SetInitialPositionTo(Input.mousePosition);
#endif
#if UNITY_ANDROID
            //TODO When select a button disable RotateAvatar
            if (Input.touchCount > 0)
            {
                SetInitialPositionTo(Input.GetTouch(0).position);
            }
#endif
        }
        else
        {
    #if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                IncreaseOffsetXBy(initialPositionCache , Input.mousePosition);
            }
            if(Input.GetMouseButtonUp(0))
            {
                ResetInitialPosition();
            }
            else
                initialPositionCache = Input.mousePosition;
#endif
#if UNITY_ANDROID

            if (Input.touchCount > 0)
            {
                isTouchEnabled = true;
                cachedTouch = Input.GetTouch(0);
                if(cachedTouch.phase == TouchPhase.Moved)
                    IncreaseOffsetXBy(initialPositionCache , cachedTouch.position);
                if(cachedTouch.phase == TouchPhase.Ended || cachedTouch.phase == TouchPhase.Canceled)
                {
                    ResetInitialPosition();
                }
                else
                    initialPositionCache = cachedTouch.position;
            }
            else
            {
                if(isTouchEnabled)
                {
                    isTouchEnabled = false;
                    ResetInitialPosition();
                }
            }
        }
#endif
        
    }

    private void IncreaseOffsetXBy(Vector3 initialPositionCache, Vector3 currentPosition)
    {
        rotationController.AddToOffsetX(currentPosition.x - initialPositionCache.x);
    }

    private void ResetInitialPosition()
    {
        initialPositionCache = Vector3.zero;
        isInitialPositionSet = false;
    }

    private void SetInitialPositionTo(Vector3 mousePosition)
    {
        initialPositionCache = mousePosition;
        if (!isInitialPositionSet)
        {
            isInitialPositionSet = true;
            rotationController.ResetOffset();
            rotationController.EnableRotation();
        }
    }
}
