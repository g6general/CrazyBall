using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sphere : MonoBehaviour
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

    private Parameters mParameters;
    private ProgressBar mProgressBar;

    private Color mColor;

    private Vector3 mDirection;
    private bool nIsButtonClicked = false;

    void init()
    {
        mProgressBar = GameObject.Find("Canvas").GetComponent<ProgressBar>();
        
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
        var correctionCoefY = mScale * 0.2f;

        mDeltaY = mAmplitude / 2 + mScale / 2 + correctionCoefY;

        var startPosX = 0f;
        var startPosY = mParameters.mBlockSizeY * startOffsetCoefY + mDeltaY;
        var startPosZ = -mParameters.mBlockSizeZ * 0.5f - startOffsetCoefZ;

        mStartPoint.position = new Vector3(startPosX, startPosY, startPosZ);

        mNumberOfDestroyedRows = 0;
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

        var verticalSpeed = mVerticalSpeed;
        var verticalStep = verticalSpeed * Time.deltaTime;
        transform.Translate(mDirection * verticalStep);
        
        if (Input.GetMouseButtonDown(0))
        {
            nIsButtonClicked = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            nIsButtonClicked = false;

            mViewPoint.position = new Vector3(mViewPoint.position.x, GetCurrentHeight() + mDeltaY, mViewPoint.position.z);
        }

        if (nIsButtonClicked)
        {
            mViewPoint.transform.Translate(Vector3.down * mDestroySpeed);
        }

        var isLengthPassed = (transform.position.z >=
                              (mParameters.getLength() - 0.5f) * mParameters.mBlockSizeZ);

        if (isLengthPassed)
        {
            mHorizontalSpeed = 0;
            mProgressBar.Stop();
        }
    }

    private void RestartGame()
    {
        mViewPoint.position = mStartPoint.position;
        mNumberOfDestroyedRows = 0;

        var blocks = GameObject.Find("MainObject").GetComponent<Blocks>();
        blocks.CreateWall();

        mProgressBar.Reset();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var blocks = GameObject.Find("MainObject").GetComponent<Blocks>();
        blocks.DestroyUpperRow();
        ++mNumberOfDestroyedRows;

        if (collision.gameObject.name == "rigid_block")
            RestartGame();
    }
}
