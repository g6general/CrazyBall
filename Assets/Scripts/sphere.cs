using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class sphere : MonoBehaviour
{
    public Transform mStartPoint;
    public Transform mViewPoint;

    public float mHorizontalSpeed;
    public float mMaxVerticalSpeed;

    private float mAmplitude = 3f;

    private Vector3 mDirection;
    
    private bool nIsButtonClicked = false;

    void Start()
    {
        mViewPoint.position = mStartPoint.position;
        mDirection = Vector3.up;
    }
    
    void Update()
    {
        var horizontalStep = mHorizontalSpeed * Time.deltaTime;
        mViewPoint.transform.Translate(Vector3.forward * horizontalStep);

        var verticalSpeed = mMaxVerticalSpeed;
        var verticalStep = verticalSpeed * Time.deltaTime;

        if (transform.position.y <= mViewPoint.position.y - mAmplitude / 2)    //BottomY
            mDirection = Vector3.up;

        if (transform.position.y >= mViewPoint.position.y + mAmplitude / 2)    //TopY
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

        if (nIsButtonClicked)
        {
            mViewPoint.transform.Translate(Vector3.down * 5);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLISION!!");
    }
}
