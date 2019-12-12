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
    private float mDeltaY;

    private Color mColor;
    private Vector3 mDirection;

    private bool mStopCamera;

    public event GameEventHandlerDelegate tapUpEvent;
    public event GameEventHandlerDelegate tapDownEvent;
    public event GameEventHandlerDelegate collisionEvent;
    public event GameEventHandlerDelegate winEvent;
    public event GameEventHandlerDelegate defeatEvent;
    
    public override void Init(GamePlay.GameData gameData)
    {
        mMoveType = eMoveType.NO_MOVE;

        mStartPoint = GameObject.Find("StartPoint").GetComponent<Transform>();
        mViewPoint = GameObject.Find("ViewPoint").GetComponent<Transform>();

        mParameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        mHorizontalSpeed = mParameters.mHorizontalSpeed;
        mVerticalSpeed = mParameters.mVerticalSpeed;
        mDestroySpeed = mParameters.mDestroySpeed;
        mAmplitude = mParameters.mAmplitude;
        mScale = mParameters.mSphereScale;
        mColor = mParameters.mSphereColor;
        
        transform.localScale = new Vector3(mScale, mScale, mScale);
        GetComponent<MeshRenderer>().material.color = mColor;
        
        mDirection = Vector3.up;
        mGameData = gameData;

        mStopCamera = false;
    }

    public override void Move()
    {
        switch (mMoveType)
        {
            case eMoveType.INPLACE:
                MoveInPlace();
                break;

            case eMoveType.FORWARD:
                MoveForward();
                break;

            case eMoveType.FIRE:
                Fire();
                break;
        }
    }

    private void MoveInPlace()
    {
        if (transform.position.y <= mViewPoint.position.y - mAmplitude / 2)    //BottomY
            mDirection = Vector3.up;

        if (transform.position.y >= mViewPoint.position.y + mAmplitude / 2)    //TopY
            mDirection = Vector3.down;

        var currentHeight = (mGameData.currentWallHeight - 0.5f) * mParameters.mBlockSizeY;
        var h = transform.position.y - currentHeight - mScale / 2;
        if (h > mAmplitude)
            h = mAmplitude;

        var verticalSpeed = mVerticalSpeed * Mathf.Sqrt(1f - h / (mParameters.mSlowDownCoef * mAmplitude));
        //var verticalSpeed = mVerticalSpeed;

        var verticalStep = verticalSpeed * Time.deltaTime;
        transform.Translate(mDirection * verticalStep);
    }
    
    private void MoveForward()
    {
        MoveInPlace();

        var horizontalStep = mHorizontalSpeed * Time.deltaTime;
        mViewPoint.transform.Translate(Vector3.forward * horizontalStep);
    }
    
    private void Fire()
    {
        mStopCamera = false;
        
        var horizontalStep = mHorizontalSpeed * Time.deltaTime;
        mViewPoint.transform.Translate(Vector3.forward * horizontalStep);

        if (mGameData.cameraMoveDown)
        {
            mViewPoint.transform.Translate(Vector3.down * mDestroySpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * mDestroySpeed * Time.deltaTime);
        }
    }
    
    public override void Break()
    {
        mMoveType = eMoveType.NO_MOVE;
        
        // todo
    }

    public override void PlaceToStart(Level level)
    {
        var startOffsetCoefY = level.mBlocksInHeight - 0.5f;
        var startOffsetCoefZ = mParameters.mStartLongitudinalOffset;

        mDeltaY = mAmplitude / 2 + mScale / 2;

        var startPosX = 0f;
        var startPosY = mParameters.mBlockSizeY * startOffsetCoefY + mDeltaY;
        var startPosZ = -mParameters.mBlockSizeZ * 0.5f - startOffsetCoefZ;

        mStartPoint.position = new Vector3(startPosX, startPosY, startPosZ);
        mViewPoint.position = mStartPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "rigid_block")
        {
            if (defeatEvent != null)
                defeatEvent(new GameEvent(GameEventsList.eType.GE_DEFEAT));

            return;
        }
        
        if (mMoveType == eMoveType.FIRE)
        {
            if (collisionEvent != null)
                collisionEvent(new GameEvent(GameEventsList.eType.GE_COLLISION_OCCURRED));
        }
    }
    
    public override void UpdateObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tapDownEvent != null)
                tapDownEvent(new GameEvent(GameEventsList.eType.GE_TAP_DOWN_OCCURRED));
        }

        if (Input.GetMouseButtonUp(0))
        {
            var currentHeight = (mGameData.currentWallHeight - 0.5f) * mParameters.mBlockSizeY;
            
            mViewPoint.position = new Vector3(mViewPoint.position.x, currentHeight + mDeltaY, mViewPoint.position.z);
            
            if (tapUpEvent != null)
                tapUpEvent(new GameEvent(GameEventsList.eType.GE_TAP_UP_OCCURRED));
        }

        var isLengthPassed = (transform.position.z >= (mGameData.initialWallLength - 0.5f) *
                              mParameters.mBlockSizeZ + mGameData.blockDeltaZ * mGameData.finishOffset);

        if (isLengthPassed)
        {
            if (winEvent != null)
                winEvent(new GameEvent(GameEventsList.eType.GE_WIN));
        }

        var newLevePositionY = mStartPoint.position.y - mParameters.mBlockSizeY * mGameData.collisionCounter;
        if (mViewPoint.position.y <= newLevePositionY)
        {
            mStopCamera = true;
            mViewPoint.position = new Vector3(mViewPoint.position.x, newLevePositionY, mViewPoint.position.z);
        }
    }

    public bool IsCameraStopped()
    {
        return mStopCamera;
    }
}
