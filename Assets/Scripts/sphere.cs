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

    private Color mColor;

    private Vector3 mDirection;
    private bool nIsButtonClicked = false;

    void init()
    {
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        mHorizontalSpeed = parameters.mHorizontalSpeed;
        mVerticalSpeed = parameters.mVerticalSpeed;
        mDestroySpeed = parameters.mDestroySpeed;
        mAmplitude = parameters.mAmplitude;
        mScale = parameters.mSphereScale;
        mColor = parameters.mSphereColor;

        mStartPoint = GameObject.Find("StartPoint").GetComponent<Transform>();
        mViewPoint = GameObject.Find("ViewPoint").GetComponent<Transform>();

        var startOffsetCoefY = parameters.getHeight() - 0.5f;
        var startOffsetCoefZ = parameters.mStartLongitudinalOffset;
        var correctionCoefY = mScale * 0.15f;

        var startPosX = 0f;
        var startPosY = parameters.mBlockSizeY * startOffsetCoefY + mAmplitude / 2 + mScale / 2 + correctionCoefY;
        var startPosZ = -parameters.mBlockSizeZ * 0.5f - startOffsetCoefZ;

        mStartPoint.position = new Vector3(startPosX, startPosY, startPosZ);
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
        }

        if (nIsButtonClicked)
        {
            mViewPoint.transform.Translate(Vector3.down * mDestroySpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision!");

        var blocks = GameObject.Find("MainObject").GetComponent<Blocks>();
        blocks.DestroyUpperRaw();
    }
}
