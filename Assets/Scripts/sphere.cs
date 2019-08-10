using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class sphere : MonoBehaviour
{
    public Transform mStartPoint;
    public Transform mViewPoint;

    private float mHorizontalSpeed = 15f;
    //private float mVerticalSpeed = 20f;
    
    //private float mMinVerticalSpeed = 5f;
    private float mMaxVerticalSpeed = 35f;

    private float mTopY = 8f;
    private float mBottomY = 3.6f;
    private Vector3 mDirection;
    
    private bool nIsButtonClicked = false;

    void Start()
    {
        transform.position = mStartPoint.position;
        mDirection = Vector3.up;
    }
    
    void Update()
    {
        var horizontalStep = mHorizontalSpeed * Time.deltaTime;
        mViewPoint.transform.Translate(Vector3.forward * horizontalStep);
        
        //transform.Translate(mViewPoint.transform.position);
        transform.Translate(Vector3.forward * horizontalStep);
        //Vector3.Lerp(transform.position, mViewPoint.transform.position, Time.time);

        var verticalSpeed = mMaxVerticalSpeed;
        var verticalStep = verticalSpeed * Time.deltaTime;

        if (transform.position.y <= mBottomY)
            mDirection = Vector3.up;

        if (transform.position.y >= mTopY)
            mDirection = Vector3.down;

        transform.Translate(mDirection * verticalStep);
        
        if (Input.GetMouseButtonDown(0))
        {
            nIsButtonClicked = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            nIsButtonClicked = false;
        }
        
        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (nIsButtonClicked)
        {
            mBottomY -= 1;    //mBlockSizeY
            mTopY -= 1;       //mBlockSizeY
            
            //
            //mViewPoint.position = new Vector3(1, 2, 3);
            //
            
            mDirection = Vector3.down;
            
            var verticalCameraStep = mMaxVerticalSpeed * Time.deltaTime;
            camera.transform.Translate(Vector3.down * verticalCameraStep, Space.World);
        }
        
        var horizontalCameraStep = mHorizontalSpeed * Time.deltaTime;
        camera.transform.Translate(Vector3.forward * horizontalCameraStep, Space.World);
        //camera.transform.Translate(mViewPoint.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLISION!!");
    }
}
