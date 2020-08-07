using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]
    private bool canRotate
    {
        get
        {
            return CanRotate;
        }
        set
        {
            CanRotate = value;
            Debug.Log("canrotate is Set to " + canRotate);
        }
    }
    private bool CanRotate;
    [SerializeField]
    private float offsetX;
    public float maxRotationPerFrame = 4;
    

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            float toRotateBy = CalculateRotaion();
            transform.Rotate(0, toRotateBy, 0);
            if (offsetX == 0)
                DisableRotation();
        }
    }


    private float CalculateRotaion()
    {
        float returnValue = 0;
        if (Mathf.Abs(offsetX) >= maxRotationPerFrame)
        {
            if(Mathf.Sign(offsetX) == 1)
            {
                returnValue = -maxRotationPerFrame;
                offsetX -= maxRotationPerFrame;
            }
            else
            {
                returnValue = maxRotationPerFrame;
                offsetX += maxRotationPerFrame;
            }
        }
        else
        {
            returnValue = offsetX;
            offsetX = 0;
        }
        Debug.Log("Return Value Calculate Rotation = " + returnValue);

        return returnValue;
    }

    public void SetOffsetXTo(float offset)
    {
        offsetX = offset;
        if(offset != 0)
            canRotate = true; ;
    }

    public void AddToOffsetX(float amountToAdd)
    {
        offsetX += amountToAdd;
        canRotate = true;
    }

    public void EnableRotation()
    {
        canRotate = true;
    }

    private void DisableRotation()
    {
        canRotate = false;
    }

    public void ResetOffset()
    {
        offsetX = 0;
        canRotate = false;
    }

}
