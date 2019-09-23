using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Transform mStartPoint;
    private Transform mViewPoint;

    private float mHorizontalSpeed;
    private float mVerticalSpeed;
    private float mDestroySpeed;
    private float mAmplitude;
    private float mScale;
    private float mDeltaY;
    private float mNumberOfDestroyedRows;

    private bool mGameStoped;
    private Parameters mParameters;
    private UiSystem mUi;

    private Color mColor;

    private Vector3 mDirection;
    private bool mIsButtonClicked = false;
    private bool mSphereMovingDown = false;

    void init()
    {
        mUi = GameObject.Find("Canvas").GetComponent<UiSystem>();
        mParameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        
        mHorizontalSpeed = mParameters.mHorizontalSpeed;
        mVerticalSpeed = mParameters.mVerticalSpeed;
        mDestroySpeed = mParameters.mDestroySpeed;
        mAmplitude = mParameters.mAmplitude;
        mScale = mParameters.mSphereScale;
        mColor = mParameters.mSphereColor;

        mStartPoint = GameObject.Find("StartPoint").GetComponent<Transform>();
        mViewPoint = GameObject.Find("ViewPoint").GetComponent<Transform>();

        var startOffsetCoefY = mParameters.getHeight() - 0.5f;
        var startOffsetCoefZ = mParameters.mStartLongitudinalOffset;

        mDeltaY = mAmplitude / 2 + mScale / 2;

        var startPosX = 0f;
        var startPosY = mParameters.mBlockSizeY * startOffsetCoefY + mDeltaY;
        var startPosZ = -mParameters.mBlockSizeZ * 0.5f - startOffsetCoefZ;

        mStartPoint.position = new Vector3(startPosX, startPosY, startPosZ);

        mNumberOfDestroyedRows = 0;
        mGameStoped = false;
    }

    private float GetCurrentHeight()
    {
        return (mParameters.getHeight() - mNumberOfDestroyedRows - 0.5f) * mParameters.mBlockSizeY;
    }

    void Start()
    {
        init();
        
        transform.localScale = new Vector3(mScale, mScale, mScale);
        GetComponent<MeshRenderer>().material.color = mColor;

        mViewPoint.position = mStartPoint.position;
        mDirection = Vector3.up;
    }
    
    void Update()
    {
        if (transform.position.y <= mViewPoint.position.y - mAmplitude / 2)    //BottomY
            mDirection = Vector3.up;

        if (transform.position.y >= mViewPoint.position.y + mAmplitude / 2)    //TopY
            mDirection = Vector3.down;

        var horizontalStep = mHorizontalSpeed * Time.deltaTime;
        mViewPoint.transform.Translate(Vector3.forward * horizontalStep);

        var h = transform.position.y - GetCurrentHeight() - mScale / 2;
        if (h > mAmplitude)
            h = mAmplitude;

        var verticalSpeed = mVerticalSpeed * Mathf.Sqrt(1f - h / (mParameters.mSlowDownCoef * mAmplitude));

        var verticalStep = verticalSpeed * Time.deltaTime;
        if (!mSphereMovingDown)
            transform.Translate(mDirection * verticalStep);

        if (Input.GetMouseButtonDown(0) && !mGameStoped)
        {
            mIsButtonClicked = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mIsButtonClicked = false;
            mSphereMovingDown = false;

            mViewPoint.position = new Vector3(mViewPoint.position.x, GetCurrentHeight() + mDeltaY, mViewPoint.position.z);
        }

        if (mSphereMovingDown)
        {
            mViewPoint.transform.Translate(Vector3.down * mDestroySpeed);
        }

        var isLengthPassed = (transform.position.z >=
                              (mParameters.getLength() - 0.5f) * mParameters.mBlockSizeZ);

        if (isLengthPassed)
        {
            mHorizontalSpeed = 0;
            mUi.StopProgressBar();
            
            mUi.ActivateWinUi();
        }
    }

    public void RestartGame()
    {
        mViewPoint.position = mStartPoint.position;
        mNumberOfDestroyedRows = 0;

        var blocks = GameObject.Find("MainObject").GetComponent<Wall>();
        blocks.CreateWall();
        
        mHorizontalSpeed = mParameters.mHorizontalSpeed;
        mVerticalSpeed = mParameters.mVerticalSpeed;

        mUi.DeactivateUi();
        mUi.ResetProgressBar();
        mUi.StartProgressBar();

        mGameStoped = false;
    }

    private void StopGame()
    {
        mGameStoped = true;
        
        mHorizontalSpeed = 0;
        mVerticalSpeed = 0;

        mUi.StopProgressBar();
        mUi.ActivateUi();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "rigid_block")
        {
            StopGame();
        }

        if (mIsButtonClicked)
        {
            mSphereMovingDown = true;
            
            var blocks = GameObject.Find("MainObject").GetComponent<Wall>();
            blocks.DestroyUpperRow();
            ++mNumberOfDestroyedRows;

            Handheld.Vibrate();
        }
    }
}
