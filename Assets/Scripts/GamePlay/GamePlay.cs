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
    private Dictionary<string, ObjectBase> mGameObjects;

    public GamePlay(Parameters parameters)
    {
        mProfile = new ProfileSystem();
        mLevels = new LevelSystem(false, false);
        mCheats = new CheatsSystem();
        mUi = new UiSystem();
        
        mParameters = parameters;

        mGameObjects = new Dictionary<string, ObjectBase>();
        mGameObjects.Add("Hero", GameObject.Find("Sphere").GetComponent<Ball>());
        mGameObjects.Add("Road", GameObject.Find("MainObject").GetComponent<Wall>());
    }
    
    public void BeforeBeginSession()
    {
        mUi.SetScreen(UiSystem.eMode.INTRO_SCREEN);
        
        mLevels.testEvent += GameEventHandler;
        
        mProfile.LoadProfile();
        mLevels.LoadLevels();
        mLevels.SetLevelForStart(mProfile.GetSave().currentLevel);

        foreach (var objectBase in mGameObjects)
        {
            objectBase.Value.Init();
        }
    }

    public void BeginSession()
    {
        mUi.SetScreen(UiSystem.eMode.INTRO_SCREEN);
        //SetEventSelf(GameEventsList.eType.GE_GAME_READY);
    }

    public void Session()
    {
        CheckUiEvents();
        CheckGameEvents();

        ((Ball)mGameObjects["Hero"]).Move();
        ResetEvent();
    }

    private void CheckUiEvents()
    {
        if (IsCurrentEvent(GameEventsList.eType.GE_GAME_READY))
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
            
            var level = mLevels.GetCurrentLevel();
            ((Wall)mGameObjects["Road"]).Build(level);
            
            ((Ball)mGameObjects["Hero"]).PlaceToStart();
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.INPLACE);
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_REMOVE_AD_BUTTON))
        {
            // purchase in store
            // todo
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_SHOW_SHOP_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.SHOP_SCREEN);
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_HIDE_SHOP_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_SHOW_SETTINGS_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.SETTINGS_SCREEN);
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_HIDE_SETTINGS_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_PLAY_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.GAME_SCREEN);
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.FORWARD);
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_SHOW_AD_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.AD_SCREEN);
        }
    }
    
    private void CheckGameEvents()
    {
        if (IsCurrentEvent(GameEventsList.eType.GE_TAP_DOWN_OCCURRED))
        {
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.FIRE);
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_TAP_UP_OCCURRED))
        {
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.FORWARD);
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_COLLISION_OCCURRED))
        {
            ((Wall)mGameObjects["Road"]).DestroyUpperRow();
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_WIN))
        {
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.INPLACE);
            mLevels.LevelUp();
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_SCREEN);
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_DEFEAT))
        {
            ((Ball)mGameObjects["Hero"]).Break();
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_SCREEN);
        }
    }
    
    public void EndSession()
    {
        mProfile.UnloadProfile();
    }
}
