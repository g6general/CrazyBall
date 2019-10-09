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
        SetEventSelf(GameEventsList.eType.GE_GAME_READY);
    }

    public void Session()
    {
        if (IsCurrentEvent(GameEventsList.eType.GE_GAME_READY))
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
            
            var level = mLevels.GetCurrentLevel();
            mRoad.Build(level);
            
            mHero.PlaceToStart();
            mHero.SetMoveType(HeroBase.eMoveType.INPLACE);
            
            ResetEvent();
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_REMOVE_AD_BUTTON))
        {
            // purchase in store
            // todo
            
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_SHOW_SHOP_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.SHOP_SCREEN);
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_HIDE_SHOP_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_SHOW_SETTINGS_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.SETTINGS_SCREEN);
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_HIDE_SETTINGS_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_PLAY_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.GAME_SCREEN);
            mHero.SetMoveType(HeroBase.eMoveType.FORWARD);
            ResetEvent();
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_TAP_DOWN_OCCURRED))
        {
            mHero.SetMoveType(HeroBase.eMoveType.FIRE);
            ResetEvent();
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_TAP_UP_OCCURRED))
        {
            mHero.SetMoveType(HeroBase.eMoveType.FORWARD);
            ResetEvent();
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_COLLISION_OCCURRED))
        {
            mRoad.DestroyUpperRow();
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_WIN))
        {
            mHero.SetMoveType(HeroBase.eMoveType.INPLACE);
            mLevels.LevelUp();
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_SCREEN);
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_DEFEAT))
        {
            mHero.Break();
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_SCREEN);
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_SHOW_AD_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.AD_SCREEN);
            ResetEvent();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_HIDE_AD_BUTTON))
        {
            SetEventSelf(GameEventsList.eType.GE_GAME_READY);
        }

        mHero.Move();
    }
    
    public void EndSession()
    {
        mProfile.UnloadProfile();
    }
}
