﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoadBase : ObjectBase
{
    protected List<List<GameObject>> mBlocks;
    
    protected GameObject mRigidBlockPref;
    protected GameObject mSoftBlockPref;

    protected Parameters mParameters;

    protected int mCurrentBlocksInHeight;
    protected int mBlocksInHeight;
    protected int mBlocksInLength;
    protected int mFinishOffset;

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
