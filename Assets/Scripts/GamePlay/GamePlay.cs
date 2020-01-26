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
    private bool mGameProcess;
    
    private Timer mTimer;
    public GameData mGameData;

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

        public void Set(uint numberOfLoops, GameEventsList.eType deferredEvent)
        {
            mDelay = numberOfLoops;
            mDeferredEvent = deferredEvent;

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

    public struct GameData
    {
        public float currentWallHeight;
        public float initialWallHeight;
        public float initialWallLength;
        public uint collisionCounter;
        public bool cameraMoveDown;
        public int finishOffset;
        public float blockDeltaZ;
    }

    public GamePlay(Parameters parameters)
    {
        mProfile = new ProfileSystem();
        mLevels = new LevelSystem(false, false);
        mCheats = new CheatsSystem();
        mUi = new UiSystem();
        
        mParameters = parameters;
        mGameProcess = false;

        mGameObjects = new Dictionary<string, ObjectBase>();
        mGameObjects.Add("Hero", GameObject.Find("Sphere").GetComponent<Ball>());
        mGameObjects.Add("Road", GameObject.Find("MainObject").GetComponent<Wall>());

        mTimer = new Timer(this);

        mGameData.collisionCounter = 0;
    }
    
    public void BeforeBeginSession()
    {
        mUi.mBriefingScreen.playButtonPressed += GameEventHandler;
        mUi.mBriefingScreen.shopButtonPressed += GameEventHandler;
        mUi.mBriefingScreen.settingsButtonPressed += GameEventHandler;
        mUi.mBriefingScreen.noAdsButtonPressed += GameEventHandler;
        
        mUi.mShopScreen.exitButtonPressed += GameEventHandler;
        mUi.mShopScreen.purchase1ButtonPressed += GameEventHandler;
        mUi.mShopScreen.purchase2ButtonPressed += GameEventHandler;
        mUi.mShopScreen.purchase3ButtonPressed += GameEventHandler;
        mUi.mShopScreen.noAdsButtonPressed += GameEventHandler;
        mUi.mShopScreen.showAdsButtonPressed += GameEventHandler;
        
        mUi.mSettingsScreen.exitButtonPressed += GameEventHandler;
        mUi.mSettingsScreen.soundButtonPressed += GameEventHandler;
        mUi.mSettingsScreen.vibroButtonPressed += GameEventHandler;
        
        mUi.mDebriefingWinScreen.nextButtonPressed += GameEventHandler;
        mUi.mDebriefingDefeatScreen.restartButtonPressed += GameEventHandler;

        ((Ball) mGameObjects["Hero"]).tapUpEvent += GameEventHandler;
        ((Ball) mGameObjects["Hero"]).tapDownEvent += GameEventHandler;
        ((Ball) mGameObjects["Hero"]).collisionEvent += GameEventHandler;
        ((Ball) mGameObjects["Hero"]).defeatEvent += GameEventHandler;
        ((Ball) mGameObjects["Hero"]).winEvent += GameEventHandler;
    }

    public void BeginSession()
    {
        SetEventSelf(GameEventsList.eType.GE_GAME_LOADING);
    }

    public void Session()
    {
        CheckTimer();

        if (mGameProcess)
        {
            UpdateGameObjects();
            CheckGameEvents();
        }
        
        UpdateGameData();
        CheckUiEvents();

        ((Ball) mGameObjects["Hero"]).Move();

        ResetEvent();
    }

    private void UpdateGameData()
    {
        mGameData.currentWallHeight = ((Wall) mGameObjects["Road"]).CurrentHeight();
        mGameData.initialWallHeight = ((Wall) mGameObjects["Road"]).Height();
        mGameData.initialWallLength = ((Wall) mGameObjects["Road"]).Length();
        mGameData.finishOffset = ((Wall) mGameObjects["Road"]).FinishOffset();
        mGameData.blockDeltaZ = ((Wall) mGameObjects["Road"]).BlockDeltaZ();
        mGameData.cameraMoveDown = !((Ball) mGameObjects["Hero"]).IsCameraStopped();
        
        foreach (var objectBase in mGameObjects)
        {
            objectBase.Value.UpdateGameData(mGameData);
        }
    }

    private void UpdateGameObjects()
    {
        foreach (var objectBase in mGameObjects)
        {
            objectBase.Value.UpdateObject();
        }
        
        mUi.ProgressProcess();
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
                objectBase.Value.Init(mGameData);
            }

            mTimer.Set(100, GameEventsList.eType.GE_GAME_READY);
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_GAME_READY))
        {
            mUi.SetScreen(UiSystem.eMode.BRIEFING_SCREEN);
            mUi.ProgressReset();
            
            var level = mLevels.GetCurrentLevel();
            ((Wall)mGameObjects["Road"]).Build(level);
            
            ((Ball)mGameObjects["Hero"]).PlaceToStart(level);
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.INPLACE);
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_REMOVE_AD_BUTTON))
        {
            Debug.Log("Purchase: remove ads");

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

        if (IsCurrentEvent(GameEventsList.eType.GE_START_GAME))
        {
            mUi.SetScreen(UiSystem.eMode.GAME_SCREEN);

            float levelLength = mGameData.initialWallLength * mParameters.mBlockSizeZ +
                                mGameData.blockDeltaZ * mGameData.finishOffset;

            mUi.ProgressStart(mParameters.mHorizontalSpeed, levelLength);
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.FORWARD);
            mGameProcess = true;
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_SHOW_AD_BUTTON))
        {
            mUi.SetScreen(UiSystem.eMode.ADS_SCREEN);
            mTimer.Set(100, GameEventsList.eType.GE_GAME_READY);
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_PURCHASE_1_BUTTON))
        {
            Debug.Log("Purchase: shop purchase 1");

            // todo
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_PURCHASE_2_BUTTON))
        {
            Debug.Log("Purchase: shop purchase 2");

            // todo
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_PURCHASE_3_BUTTON))
        {
            Debug.Log("Purchase: shop purchase 3");

            // todo
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_SOUND_BUTTON))
        {
            Debug.Log("UI: sound button");

            // todo
        }
        
        if (IsCurrentEvent(GameEventsList.eType.GE_VIBRO_BUTTON))
        {
            Debug.Log("UI: vibro button");

            // todo
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
            Handheld.Vibrate();
            ++mGameData.collisionCounter;
            mGameData.cameraMoveDown = true;
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_WIN))
        {
            ((Ball)mGameObjects["Hero"]).SetMoveType(HeroBase.eMoveType.INPLACE);
            mLevels.LevelUp();
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_WIN_SCREEN);
            mUi.ProgressStop();
            mGameProcess = false;
            mGameData.collisionCounter = 0;
        }

        if (IsCurrentEvent(GameEventsList.eType.GE_DEFEAT))
        {
            ((Ball)mGameObjects["Hero"]).Break();
            mUi.SetScreen(UiSystem.eMode.DEBRIEFING_DEFEAT_SCREEN);
            mUi.ProgressStop();
            mGameProcess = false;
            mGameData.collisionCounter = 0;
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
