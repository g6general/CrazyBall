using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using System.IO;

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

    private string GetConfigData(string fileName)
    {
        var configPath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
        string data;
            
#if UNITY_EDITOR || UNITY_IOS
        data = File.ReadAllText(configPath);
#elif UNITY_ANDROID
            WWW reader = new WWW (configPath);
            while (!reader.isDone) {}
            data = reader.text;
#endif
        return data;
    }

    public void LoadLevels()
    {
        var levels = XElement.Parse(GetConfigData("levels.xml"));

        var curLevel = levels.FirstNode;
        while (curLevel != null)
        {
            var nodeLevel = (XElement) curLevel;

            var columnsAttr = nodeLevel.FirstAttribute;
            var finishAttr = columnsAttr.NextAttribute;
            var rowsAttr = finishAttr.NextAttribute;

            var level = new Level();
            level.mBlocksInHeight = Convert.ToInt32(rowsAttr.Value);
            level.mBlocksInLength = Convert.ToInt32(columnsAttr.Value);
            level.mFinishOffset = Convert.ToInt32(finishAttr.Value);

            var pos = nodeLevel.FirstNode;
            while (pos != null)
            {
                var nodePos = (XElement) pos;

                var iPosAttr = nodePos.FirstAttribute;
                var jPosAttr = iPosAttr.NextAttribute;
                var valueAttr = jPosAttr.NextAttribute;
                
                var iPos = Convert.ToInt32(iPosAttr.Value);
                var jPos = Convert.ToInt32(jPosAttr.Value);

                if (valueAttr.Value == "rigid")
                {
                    var mirroredPosI = level.mBlocksInHeight - iPos - 1;
                    level.mBarriers.Add(new RoadBase.Position(mirroredPosI, jPos)); 
                }

                pos = pos.NextNode;
            }
            
            mLevels.Add(level);
                
            curLevel = curLevel.NextNode;
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

public class Level
{
    public Level()
    {
        mBarriers = new List<RoadBase.Position>();
        mBlocksInLength = 0;
        mBlocksInHeight = 0;
        mFinishOffset = 0;
    }
    
    public List<RoadBase.Position> mBarriers;
    
    public int mBlocksInLength;
    public int mBlocksInHeight;
    public int mFinishOffset;
}
