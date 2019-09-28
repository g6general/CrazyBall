using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private bool mShuffled;
    private bool mLooped;
    
    private uint mCurrentLevelNumber;
    private List<Level> mLevels;

    public LevelSystem(bool shuffled, bool looped)
    {
        mShuffled = shuffled;
        mLooped = looped;
    }

    public void LoadLevels()
    {
        // todo
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
        return 0;
    }

    public Level GetCurrentLevel()
    {
        // todo
        return new Level();
    }

    public void LevelUp()
    {
        // todo
    }

}

public struct Level
{
    // todo
}
