using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay
{
    private ProfileSystem mProfile;
    private LevelSystem mLevels;
    private CheatsSystem mCheats;
    private UiSystem mUi;

    private Parameters mParameters;
    private Ball mHero;
    private Wall mRoad;
    
    public GamePlay(Parameters parameters)
    {
        mProfile = new ProfileSystem();
        mLevels = new LevelSystem(false, false);
        mCheats = new CheatsSystem();
        mUi = new UiSystem();
        
        mParameters = parameters;
        mHero = GameObject.Find("Sphere").GetComponent<Ball>();
        mRoad = GameObject.Find("MainObject").GetComponent<Wall>();
    }
    
    public void BeforeBeginSession()
    {
        mProfile.LoadProfile();
        mLevels.LoadLevels();
        
        //mHero.Init();    //temp
        mRoad.Init();
    }

    public void BeginSession()
    {
        // todo
    }

    public void Session()
    {
        // todo
    }
    
    public void EndSession()
    {
        mProfile.UnloadProfile();
    }
}
