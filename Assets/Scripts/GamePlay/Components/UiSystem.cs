using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSystem : MonoBehaviour
{
    private RectTransform mProgressBar;
    private float mSphereSpeedCoef;

    private GameObject mButton;
    private GameObject mTextFail;
    private GameObject mTextWin;

    void Start()
    {
        mProgressBar = GameObject.Find("ProgressBarFilled").GetComponent<RectTransform>();
        
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        var levelLength = parameters.getLength() * parameters.mBlockSizeZ;
        var sphereSpeed = parameters.mHorizontalSpeed;
        
        mSphereSpeedCoef = sphereSpeed / levelLength;

        mButton = GameObject.Find("Button");
        mButton.SetActive(false);

        var b = mButton.GetComponent<Button>();
        b.onClick.AddListener(OnButtonClicked);
        
        mTextFail = GameObject.Find("TextFail");
        mTextFail.SetActive(false);
        
        mTextWin = GameObject.Find("TextWin");
        mTextWin.SetActive(false);
    }

    void Update()
    {
        var horizontalStep = mSphereSpeedCoef * Time.deltaTime;
        mProgressBar.localScale = new Vector3(mProgressBar.localScale.x + horizontalStep, mProgressBar.localScale.y);
    }

    public void ResetProgressBar()
    {
        mProgressBar.localScale = new Vector3(0, mProgressBar.localScale.y);
    }
    
    public void StartProgressBar()
    {
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        var levelLength = parameters.getLength() * parameters.mBlockSizeZ;
        var sphereSpeed = parameters.mHorizontalSpeed;
        
        mSphereSpeedCoef = sphereSpeed / levelLength;
    }

    public void StopProgressBar()
    {
        mSphereSpeedCoef = 0;
    }

    public void ActivateUi()
    {
        mButton.SetActive(true);
        mTextFail.SetActive(true);
    }
    
    public void DeactivateUi()
    {
        mButton.SetActive(false);
        mTextFail.SetActive(false);
    }
    
    public void ActivateWinUi()
    {
        mTextWin.SetActive(true);
    }

    private void OnButtonClicked()
    {
        var sphere = GameObject.Find("Sphere").GetComponent<Ball>();
        sphere.RestartGame();
    }
}
