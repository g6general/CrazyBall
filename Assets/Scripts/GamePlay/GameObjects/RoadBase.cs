using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoadBase : MonoBehaviour
{
    protected List<List<GameObject>> mBlocks;
    
    protected GameObject mRigidBlockPref;
    protected GameObject mSoftBlockPref;

    protected Parameters mParameters;
    
    public abstract void Init();
    public abstract void Build(Level level);
    public abstract void Destroy();
    public abstract bool DestroyUpperRow();
    
    public struct Position
    {
        public int mVertical;
        public int mHorizontal;

        public Position(int vertical, int horizontal)
        {
            mVertical = vertical;
            mHorizontal = horizontal;
        }
    }
}
