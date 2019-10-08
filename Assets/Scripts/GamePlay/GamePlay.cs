using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : GameEventSubscriber
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
        mUi.SetScreen(UiSystem.eMode.INTRO_SCREEN);
        
        mLevels.testEvent += GameEventHandler;
        
        mProfile.LoadProfile();
        mLevels.LoadLevels();
        mLevels.SetLevelForStart(mProfile.GetSave().currentLevel);
        
        //mHero.Init();    //temp
        mRoad.Init();
    }

    public void BeginSession()
    {
        mUi.SetScreen(UiSystem.eMode.INTRO_SCREEN);
        mCurrentEvent = GameEventsList.eType.GE_GAME_IS_LOADED;
    }

    public void Session()
    {
        /*
        if (mCurrentEvent == GameEventsList.eType.GE_GAME_IS_LOADED)
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
            
            var level = mLevels.GetCurrentLevel();
            mRoad.Build(level);
            
            mHero.PlaceToStart();
            mHero.SetMoveType(HeroBase.eMoveType.INPLACE);
        }

        if (mCurrentEvent == GameEventsList.eType.GE_BRIEFING_PLAY_BUTTON)
        {
            mUi.SetScreen(UiSystem.eMode.GAME_SCREEN);
            mHero.SetMoveType(HeroBase.eMoveType.FORWARD);
        }
        
        // todo
        
        mHero.Move();
        */
    }
    
    public void EndSession()
    {
        mProfile.UnloadProfile();
    }
}
