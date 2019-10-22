using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSystem
{
    private Save mSave;
    private Settings mSettings;

    public ProfileSystem()
    {
        // todo

        // temp
        mSave.currentLevel = 0;
        // temp
    }

    public void LoadProfile()
    {
        // todo
    }

    public void UnloadProfile()
    {
        // todo
    }
    
    public ref Save GetSave() { return ref mSave; }
    public ref Settings GetSettings() { return ref mSettings; }
}

public struct Save
{
    public int currentLevel;


    // todo
}

public struct Settings
{
    // todo
}
