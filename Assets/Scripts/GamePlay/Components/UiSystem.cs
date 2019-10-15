using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSystem
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
    
    private Canvas mIntroScreen;
    private Canvas mBriefingScreen;
    private Canvas mGameScreen;
    private Canvas mDebriefingWinScreen;
    private Canvas mDebriefingDefeatScreen;
    private Canvas mSettingsScreen;
    private Canvas mShopScreen;
    private Canvas mAdsScreen;

    private eMode mScreenMode;

    public UiSystem()
    {
        mIntroScreen = GameObject.Find("intro_screen").GetComponent<Canvas>();
        mBriefingScreen = GameObject.Find("briefing_screen").GetComponent<Canvas>();
        mGameScreen = GameObject.Find("game_screen").GetComponent<Canvas>();
        mDebriefingWinScreen = GameObject.Find("debriefing_win_screen").GetComponent<Canvas>();
        mDebriefingDefeatScreen = GameObject.Find("debriefing_defeat_screen").GetComponent<Canvas>();
        mSettingsScreen = GameObject.Find("settings_screen").GetComponent<Canvas>();
        mShopScreen = GameObject.Find("shop_screen").GetComponent<Canvas>();
        mAdsScreen = GameObject.Find("ads_screen").GetComponent<Canvas>();
        
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
                SetScreenDrawing(mIntroScreen, true);
                break;
            case eMode.BRIEFING_SCREEN:
                SetScreenDrawing(mBriefingScreen, true);
                break;
            case eMode.GAME_SCREEN:
                SetScreenDrawing(mGameScreen, true);
                break;
            case eMode.DEBRIEFING_WIN_SCREEN:
                SetScreenDrawing(mDebriefingWinScreen, true);
                break;
            case eMode.DEBRIEFING_DEFEAT_SCREEN:
                SetScreenDrawing(mDebriefingDefeatScreen, true);
                break;
            case eMode.SETTINGS_SCREEN:
                SetScreenDrawing(mSettingsScreen, true);
                break;
            case eMode.SHOP_SCREEN:
                SetScreenDrawing(mShopScreen, true);
                break;
            case eMode.ADS_SCREEN:
                SetScreenDrawing(mAdsScreen, true);
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

    private void SetScreenDrawing(Canvas screen, bool mode)
    {
        screen.enabled = mode;
    }

    private void HideAllScreens()
    {
        SetScreenDrawing(mIntroScreen, false);
        SetScreenDrawing(mBriefingScreen, false);
        SetScreenDrawing(mGameScreen, false);
        SetScreenDrawing(mDebriefingWinScreen, false);
        SetScreenDrawing(mDebriefingDefeatScreen, false);
        SetScreenDrawing(mSettingsScreen, false);
        SetScreenDrawing(mShopScreen, false);
        SetScreenDrawing(mAdsScreen, false);
    }
}
