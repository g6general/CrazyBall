using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    public int mBlockSizeX;
    public int mBlockSizeY;
    public int mBlockSizeZ;

    public int mBlocksInLength;
    public int mBlocksInHeight;

    public Color mSoftBlockColor;
    public Color mRigidBlockColor;
    
    public List<Position> mBlocks;
    
    public Color mSphereColor;
    public int mSphereRadius;
    
    public float mHorizontalSpeed;
    public float mVerticalSpeed;
    public int mDestroySpeed;

    public float mAmplitude;

    public int mCameraPosX;
    public int mCameraPosY;
    public int mCameraPosZ;

    public int mCameraRotX;
    public int mCameraRotY;
    public int mCameraRotZ;
}
