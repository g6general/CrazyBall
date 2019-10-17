using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : HeroBase
{
    private float mHorizontalSpeed;
    private float mVerticalSpeed;
    private float mDestroySpeed;
    private float mAmplitude;
    private float mScale;
    private Color mColor;
    
    public override void Init()
    {
        mMoveType = eMoveType.NO_MOVE;

        mParameters = GameObject.Find("MainObject").GetComponent<Parameters>();

        mStartPoint = GameObject.Find("StartPoint").GetComponent<Transform>();
        mViewPoint = GameObject.Find("ViewPoint").GetComponent<Transform>();

        mHorizontalSpeed = mParameters.mHorizontalSpeed;
        mVerticalSpeed = mParameters.mVerticalSpeed;
        mDestroySpeed = mParameters.mDestroySpeed;
        mAmplitude = mParameters.mAmplitude;
        mScale = mParameters.mSphereScale;
        mColor = mParameters.mSphereColor;
    }

    public override void Move()
    {
        //Debug.Log("Move Ball!");
    }
    
    public override void Break()
    {
        Debug.Log("Break Ball!");
    }

    public override void PlaceToStart()
    {
        // todo
        
        /*
        var startOffsetCoefY = mParameters.getHeight() - 0.5f;
        var startOffsetCoefZ = mParameters.mStartLongitudinalOffset;

        mDeltaY = mAmplitude / 2 + mScale / 2;

        var startPosX = 0f;
        var startPosY = mParameters.mBlockSizeY * startOffsetCoefY + mDeltaY;
        var startPosZ = -mParameters.mBlockSizeZ * 0.5f - startOffsetCoefZ;

        mStartPoint.position = new Vector3(startPosX, startPosY, startPosZ);
        */
        
        //mViewPoint.position = mStartPoint.position;
    }
}
