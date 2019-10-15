using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : GameEventSender
{
    private bool mShuffled;
    private bool mLooped;
    
    private int mCurrentLevelNumber;
    private List<Level> mLevels;
    
    // temp
    public event GameEventHandlerDelegate testEvent;
    // temp

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
        if (testEvent != null)
            testEvent(new GameEvent(GameEventsList.eType.GE_WIN));
        // temp
        
        // temp
        var firstLevel = new Level();
        firstLevel.mBarriers = new List<RoadBase.Position>();
        mLevels.Add(firstLevel);

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

        var height = tempBlockPositions.GetUpperBound(0) + 1;
        var length = tempBlockPositions.Length / height;

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < length; ++j)
            {
                if (tempBlockPositions[i, j] == 1)
                    mLevels[0].mBarriers.Add(new RoadBase.Position(height - 1 - i, j));
            }
        }
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
}
