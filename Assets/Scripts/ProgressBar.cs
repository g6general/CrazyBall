using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public RectTransform progressBar;
    public bool ballMoving = true;
    public int levelLength;
    public float blockLength;
    public float speed;

    private float progressBarMovementSpeed;

    private void Start()
    {
        progressBarMovementSpeed = speed / (levelLength * blockLength);
    }

    void Update()
    {
        
        if (ballMoving)
        {
            progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x+progressBarMovementSpeed*Time.deltaTime, 100);
        }
    }
}
