using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : GameEventSender
{
    private bool mShuffled;
    private bool mLooped;
    
    private uint mCurrentLevelNumber;
    private List<Level> mLevels;
    
    //
    public event GameEventHandlerDelegate testEvent;
    //

    public LevelSystem(bool shuffled, bool looped)
    {
        mShuffled = shuffled;
        mLooped = looped;
    }

    public void SetLevelForStart(int index)
    {
        // todo
    }

    public void LoadLevels()
    {
        // todo
        
        // temp
        if (testEvent != null)
            testEvent(new GameEvent(GameEventsList.eType.GE_WIN));
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
