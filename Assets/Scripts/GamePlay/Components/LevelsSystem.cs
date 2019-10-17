using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : GameEventSender
{
    private bool mShuffled;
    private bool mLooped;
    
    private int mCurrentLevelNumber;
    private List<Level> mLevels;

    public LevelSystem(bool shuffled, bool looped)
    {
        mShuffled = shuffled;
        mLooped = looped;
        
        mLevels = new List<Level>();
    }

    public void SetLevelForStart(int index)
    {
        // todo
        
        // temp
        mCurrentLevelNumber = index;
        // temp
    }

    public void LoadLevels()
    {
        // todo

        // temp
        var firstLevel = new Level();
        firstLevel.mBarriers = new List<RoadBase.Position>();

        uint[,] tempBlockPositions = {
            { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        firstLevel.mBlocksInHeight = tempBlockPositions.GetUpperBound(0) + 1;
        firstLevel.mBlocksInLength = tempBlockPositions.Length / firstLevel.mBlocksInHeight;

        for (var i = 0; i < firstLevel.mBlocksInHeight; i++)
        {
            for (var j = 0; j < firstLevel.mBlocksInLength; ++j)
            {
                if (tempBlockPositions[i, j] == 1)
                    firstLevel.mBarriers.Add(new RoadBase.Position(firstLevel.mBlocksInHeight - 1 - i, j));
            }
        }
        
        mLevels.Add(firstLevel);
        // temp
    }

    public int GetNumberOfLevels()
    {
        if (mLooped)
            return -1;

        return mLevels.Count;
    }

    public int GetCurrentLevelNumber()
    {
        // todo
        
        // temp
        return mCurrentLevelNumber;
        // temp
    }

    public Level GetCurrentLevel()
    {
        // todo

        // temp
        return mLevels[0];
        // temp
    }

    public void LevelUp()
    {
        // todo
    }
}

public struct Level
{
    // todo
    
    public List<RoadBase.Position> mBarriers;
    
    public int mBlocksInLength;
    public int mBlocksInHeight;
}
