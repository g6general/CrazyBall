using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiSystem : GameEventSender
{
    public enum eMode
    {
        INTRO_SCREEN,
        BRIEFING_SCREEN,
        GAME_SCREEN,
        DEBRIEFING_WIN_SCREEN,
        DEBRIEFING_DEFEAT_SCREEN,
        SETTINGS_SCREEN,
        SHOP_SCREEN,
        ADS_SCREEN,
        NO_SCREEN
    }
    
    private eMode mScreenMode;
    
    public BriefingScreen mBriefingScreen;
    public IntroScreen mIntroScreen;
    public ShopScreen mShopScreen;
    public SettingsScreen mSettingsScreen;
    public AdsScreen mAdsScreen;
    public GameScreen mGameScreen;
    public DebriefingWinScreen mDebriefingWinScreen;
    public DebriefingDefeatScreen mDebriefingDefeatScreen;

    public UiSystem()
    {
        mBriefingScreen = new BriefingScreen();
        mIntroScreen = new IntroScreen();
        mShopScreen = new ShopScreen();
        mSettingsScreen = new SettingsScreen();
        mAdsScreen = new AdsScreen();
        mGameScreen = new GameScreen();
        mDebriefingWinScreen = new DebriefingWinScreen();
        mDebriefingDefeatScreen = new DebriefingDefeatScreen();
        
        HideAllScreens();
        mScreenMode = eMode.NO_SCREEN;
    }

    public void ProgressStart()
    {
        // todo
    }
    
    public void ProgressStop()
    {
        // todo
    }
    
    public void ProgressReset()
    {
        // todo
    }
    
    public void SetScreen(eMode screen)
    {
        mScreenMode = screen;
        HideAllScreens();

        switch (mScreenMode)
        {
            case eMode.INTRO_SCREEN:
                mIntroScreen.Show();
                break;
            case eMode.BRIEFING_SCREEN:
                mBriefingScreen.Show();
                break;
            case eMode.GAME_SCREEN:
                mGameScreen.Show();
                break;
            case eMode.DEBRIEFING_WIN_SCREEN:
                mDebriefingWinScreen.Show();
                break;
            case eMode.DEBRIEFING_DEFEAT_SCREEN:
                mDebriefingDefeatScreen.Show();
                break;
            case eMode.SETTINGS_SCREEN:
                mSettingsScreen.Show();
                break;
            case eMode.SHOP_SCREEN:
                mShopScreen.Show();
                break;
            case eMode.ADS_SCREEN:
                mAdsScreen.Show();
                break;
            case eMode.NO_SCREEN:
            default:
                break;
        }
    }
    
    public eMode GetScreen()
    {
        return mScreenMode;
    }

    private void HideAllScreens()
    {
        mIntroScreen.Hide();
        mBriefingScreen.Hide();
        mGameScreen.Hide();
        mDebriefingWinScreen.Hide();
        mDebriefingDefeatScreen.Hide();
        mSettingsScreen.Hide();
        mShopScreen.Hide();
        mAdsScreen.Hide();
    }

    public class Screen
    {
        protected Canvas mCanvas;

        public void Show() { mCanvas.enabled = true; }
        public void Hide() { mCanvas.enabled = false; }
    }

    public class IntroScreen : Screen
    {
        public IntroScreen()
        {
            mCanvas = GameObject.Find("intro_screen").GetComponent<Canvas>();
        }
    }

    public class AdsScreen : Screen
    {
        public AdsScreen()
        {
            mCanvas = GameObject.Find("ads_screen").GetComponent<Canvas>();
        }
    }
    
    public class GameScreen : Screen
    {
        public GameScreen()
        {
            mCanvas = GameObject.Find("game_screen").GetComponent<Canvas>();
        }
    }

    public class BriefingScreen : Screen
    {
        public Button mPlayButton;
        public Button mShopButton;
        public Button mSettingsButton;
        public Button mNoAdsButton;
        
        public event GameEventHandlerDelegate playButtonPressed;
        public event GameEventHandlerDelegate shopButtonPressed;
        public event GameEventHandlerDelegate settingsButtonPressed;
        public event GameEventHandlerDelegate noAdsButtonPressed;

        public BriefingScreen()
        {
            mCanvas = GameObject.Find("briefing_screen").GetComponent<Canvas>();

            mPlayButton = GameObject.Find("Button_play").GetComponent<Button>();
            mPlayButton.onClick.AddListener(onPlayBtnClick);
            
            mShopButton = GameObject.Find("Button_shop").GetComponent<Button>();
            mShopButton.onClick.AddListener(onShopBtnClick);
            
            mSettingsButton = GameObject.Find("Button_settings").GetComponent<Button>();
            mSettingsButton.onClick.AddListener(onSettingsBtnClick);
            
            mNoAdsButton = GameObject.Find("Button_no_ads").GetComponent<Button>();
            mNoAdsButton.onClick.AddListener(onNoAdsBtnClick);
        }
        
        public void onPlayBtnClick()
        {
            if (playButtonPressed != null)
                playButtonPressed(new GameEvent(GameEventsList.eType.GE_START_GAME));
        }
        
        public void onShopBtnClick()
        {
            if (shopButtonPressed != null)
                shopButtonPressed(new GameEvent(GameEventsList.eType.GE_SHOW_SHOP_BUTTON));
        }
        
        public void onSettingsBtnClick()
        {
            if (settingsButtonPressed != null)
                settingsButtonPressed(new GameEvent(GameEventsList.eType.GE_SHOW_SETTINGS_BUTTON));
        }
        
        public void onNoAdsBtnClick()
        {
            if (noAdsButtonPressed != null)
                noAdsButtonPressed(new GameEvent(GameEventsList.eType.GE_REMOVE_AD_BUTTON));
        }
    }
    
    public class ShopScreen : Screen
    {
        public Button mExitButton;
        public Button mPurchase1Button;
        public Button mPurchase2Button;
        public Button mPurchase3Button;
        public Button mNoAdsButton;
        public Button mShowAdsButton;
        
        public event GameEventHandlerDelegate exitButtonPressed;
        public event GameEventHandlerDelegate purchase1ButtonPressed;
        public event GameEventHandlerDelegate purchase2ButtonPressed;
        public event GameEventHandlerDelegate purchase3ButtonPressed;
        public event GameEventHandlerDelegate noAdsButtonPressed;
        public event GameEventHandlerDelegate showAdsButtonPressed;

        public ShopScreen()
        {
            mCanvas = GameObject.Find("shop_screen").GetComponent<Canvas>();

            mExitButton = GameObject.Find("Button_exit_shop").GetComponent<Button>();
            mExitButton.onClick.AddListener(onExitBtnClick);
            
            mPurchase1Button = GameObject.Find("Button_purchase_1").GetComponent<Button>();
            mPurchase1Button.onClick.AddListener(onPurchase1BtnClick);
            
            mPurchase2Button = GameObject.Find("Button_purchase_2").GetComponent<Button>();
            mPurchase2Button.onClick.AddListener(onPurchase2BtnClick);
            
            mPurchase3Button = GameObject.Find("Button_purchase_3").GetComponent<Button>();
            mPurchase3Button.onClick.AddListener(onPurchase3BtnClick);
            
            mNoAdsButton = GameObject.Find("Button_remove_ad").GetComponent<Button>();
            mNoAdsButton.onClick.AddListener(onNoAdsBtnClick);
            
            mShowAdsButton = GameObject.Find("Button_show_ad").GetComponent<Button>();
            mShowAdsButton.onClick.AddListener(onShowAdsBtnClick);
        }
        
        public void onExitBtnClick()
        {
            if (exitButtonPressed != null)
                exitButtonPressed(new GameEvent(GameEventsList.eType.GE_HIDE_SHOP_BUTTON));
        }
        
        public void onPurchase1BtnClick()
        {
            if (purchase1ButtonPressed != null)
                purchase1ButtonPressed(new GameEvent(GameEventsList.eType.GE_PURCHASE_1_BUTTON));
        }
        
        public void onPurchase2BtnClick()
        {
            if (purchase2ButtonPressed != null)
                purchase2ButtonPressed(new GameEvent(GameEventsList.eType.GE_PURCHASE_2_BUTTON));
        }
        
        public void onPurchase3BtnClick()
        {
            if (purchase3ButtonPressed != null)
                purchase3ButtonPressed(new GameEvent(GameEventsList.eType.GE_PURCHASE_3_BUTTON));
        }

        public void onNoAdsBtnClick()
        {
            if (noAdsButtonPressed != null)
                noAdsButtonPressed(new GameEvent(GameEventsList.eType.GE_REMOVE_AD_BUTTON));
        }
        
        public void onShowAdsBtnClick()
        {
            if (showAdsButtonPressed != null)
                showAdsButtonPressed(new GameEvent(GameEventsList.eType.GE_SHOW_AD_BUTTON));
        }
    }
    
    public class SettingsScreen : Screen
    {
        public Button mExitButton;
        public Button mSoundButton;
        public Button mVibroButton;

        public event GameEventHandlerDelegate exitButtonPressed;
        public event GameEventHandlerDelegate soundButtonPressed;
        public event GameEventHandlerDelegate vibroButtonPressed;

        public SettingsScreen()
        {
            mCanvas = GameObject.Find("settings_screen").GetComponent<Canvas>();

            mExitButton = GameObject.Find("Button_exit_settings").GetComponent<Button>();
            mExitButton.onClick.AddListener(onExitBtnClick);
            
            mSoundButton = GameObject.Find("Button_sound").GetComponent<Button>();
            mSoundButton.onClick.AddListener(onSoundBtnClick);
            
            mVibroButton = GameObject.Find("Button_vibro").GetComponent<Button>();
            mVibroButton.onClick.AddListener(onVibroBtnClick);
        }
        
        public void onExitBtnClick()
        {
            if (exitButtonPressed != null)
                exitButtonPressed(new GameEvent(GameEventsList.eType.GE_HIDE_SETTINGS_BUTTON));
        }
        
        public void onSoundBtnClick()
        {
            if (soundButtonPressed != null)
                soundButtonPressed(new GameEvent(GameEventsList.eType.GE_SOUND_BUTTON));
        }
        
        public void onVibroBtnClick()
        {
            if (vibroButtonPressed != null)
                vibroButtonPressed(new GameEvent(GameEventsList.eType.GE_VIBRO_BUTTON));
        }
    }
    
    public class DebriefingWinScreen : Screen
    {
        public Button mNextButton;

        public event GameEventHandlerDelegate nextButtonPressed;

        public DebriefingWinScreen()
        {
            mCanvas = GameObject.Find("debriefing_win_screen").GetComponent<Canvas>();

            mNextButton = GameObject.Find("Button_next").GetComponent<Button>();
            mNextButton.onClick.AddListener(onNextBtnClick);
        }
        
        public void onNextBtnClick()
        {
            if (nextButtonPressed != null)
                nextButtonPressed(new GameEvent(GameEventsList.eType.GE_GAME_READY));
        }
    }
    
    public class DebriefingDefeatScreen : Screen
    {
        public Button mRestartButton;

        public event GameEventHandlerDelegate restartButtonPressed;

        public DebriefingDefeatScreen()
        {
            mCanvas = GameObject.Find("debriefing_defeat_screen").GetComponent<Canvas>();

            mRestartButton = GameObject.Find("Button_restart").GetComponent<Button>();
            mRestartButton.onClick.AddListener(onRestartBtnClick);
        }
        
        public void onRestartBtnClick()
        {
            if (restartButtonPressed != null)
                restartButtonPressed(new GameEvent(GameEventsList.eType.GE_GAME_READY));
        }
    }
}
