using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private RectTransform mProgressBar;
    private float mSphereSpeedCoef;

    void Start()
    {
        mProgressBar = GameObject.Find("ProgressBarFilled").GetComponent<RectTransform>();
        
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        var levelLength = parameters.getLength() * parameters.mBlockSizeZ;
        var sphereSpeed = parameters.mHorizontalSpeed;
        
        mSphereSpeedCoef = sphereSpeed / levelLength;
    }

    void Update()
    {
        var horizontalStep = mSphereSpeedCoef * Time.deltaTime;
        mProgressBar.localScale = new Vector3(mProgressBar.localScale.x + horizontalStep, mProgressBar.localScale.y);
    }
}
