using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GamePlay : GameEventSubscriber
{
    private ProfileSystem mProfile;
    private LevelSystem mLevels;
    private CheatsSystem mCheats;
    private UiSystem mUi;

    private Parameters mParameters;
    private Dictionary<string, ObjectBase> mGameObjects;

    private struct Timer
    {
        public GamePlay mParent;

        public uint mDelay;
        public GameEventsList.eType mDeferredEvent;

        public Timer(GamePlay parent)
        {
            mParent = parent;

            mDelay = 0;
            mDeferredEvent = GameEventsList.eType.GE_NO;
        }

        public void Set(uint numberOfLoops, GameEventsList.eType eventToSet)
        {
            mDelay = numberOfLoops;
            mDeferredEvent = eventToSet;

            mParent.SetEventSelf(GameEventsList.eType.GE_GAME_WAITING);
        }

        public void Reset()
        {
            mParent.SetEventSelf(mDeferredEvent);

            mDelay = 0;
            mDeferredEvent = GameEventsList.eType.GE_NO;
        }

        public void Check()
        {
            --mDelay;

            if (mDelay <= 0)
                Reset();
        }
    }

    private Timer mTimer;

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

        mTimer = new Timer(this);
    }
    
    public void BeforeBeginSession()
    {
        //
        mLevels.testEvent += GameEventHandler;
        //
    }

    public void BeginSession()
    {
        SetEventSelf(GameEventsList.eType.GE_GAME_LOADING);
    }

    public void Session()
    {
        CheckTimer();

        CheckUiEvents();
        CheckGameEvents();

        ((Ball)mGameObjects["Hero"]).Move();

        ResetEvent();
    }

    private void CheckUiEvents()
    {
        if (IsCurrentEvent(GameEventsList.eType.GE_GAME_LOADING))
        {
            mUi.SetScreen(UiSystem.eMode.INTRO_SCREEN);

            mProfile.LoadProfile();
            mLevels.LoadLevels();
            mLevels.SetLevelForStart(mProfile.GetSave().currentLevel);

            foreach (var objectBase in mGameObjects)
            {
                objectBase.Value.Init();
            }

            mTimer.Set(100, GameEventsList.eType.GE_GAME_READY);
        }
        
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
            mUi.SetScreen(UiSystem.eMode.ADS_SCREEN);
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
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_WIN_SCREEN);
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_DEFEAT))
        {
            ((Ball)mGameObjects["Hero"]).Break();
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_DEFEAT_SCREEN);
        }
    }

    private void CheckTimer()
    {
        if (IsCurrentEvent(GameEventsList.eType.GE_GAME_WAITING))
        {
            mTimer.Check();
        }
    }
    
    public void EndSession()
    {
        mProfile.UnloadProfile();
    }
}
