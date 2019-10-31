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

        mCurrentLevelNumber = 0;
    }

    public void SetLevelForStart(int index)
    {
        // todo
        
        mCurrentLevelNumber = index;
    }

    public void LoadLevels()
    {
        // todo
        
        var arrays = new List<uint[,]>();

        uint[,] blockPositions_1 = {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
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
        arrays.Add(blockPositions_1);

        uint[,] blockPositions_2 = {
            { 0, 0, 0, 0, 1, 1 },
            { 1, 1, 1, 0, 0, 0 },
            { 1, 1, 1, 0, 0, 0 },
            { 1, 0, 1, 1, 0, 0 },
            { 1, 1, 1, 1, 0, 0 }
        };
        arrays.Add(blockPositions_2);
        
        uint[,] blockPositions_3 = {
            { 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0 },
            { 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };
        arrays.Add(blockPositions_3);

        for (var i = 0; i < arrays.Count; i++)
        {
            var firstLevel = new Level();
            firstLevel.mBarriers = new List<RoadBase.Position>();

            firstLevel.mBlocksInHeight = arrays[i].GetUpperBound(0) + 1;
            firstLevel.mBlocksInLength = arrays[i].Length / firstLevel.mBlocksInHeight;

            for (var j = 0; j < firstLevel.mBlocksInHeight; j++)
            {
                for (var k = 0; k < firstLevel.mBlocksInLength; ++k)
                {
                    if (arrays[i][j, k] == 1)
                        firstLevel.mBarriers.Add(new RoadBase.Position(firstLevel.mBlocksInHeight - 1 - j, k));
                }
            }

            mLevels.Add(firstLevel);
        }
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

        return mCurrentLevelNumber;
    }

    public Level GetCurrentLevel()
    {
        // todo
        
        return mLevels[mCurrentLevelNumber];
    }

    public void LevelUp()
    {
        // todo
        
        ++mCurrentLevelNumber;

        if (mCurrentLevelNumber == mLevels.Count)
            mCurrentLevelNumber = 0;
    }
}

public struct Level
{
    // todo
    
    public List<RoadBase.Position> mBarriers;
    
    public int mBlocksInLength;
    public int mBlocksInHeight;
}
