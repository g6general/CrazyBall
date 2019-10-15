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
        AD_SCREEN,
        NO_SCREEN
    }

    private eMode mScreenMode;

    public UiSystem()
    {
        mScreenMode = eMode.NO_SCREEN;
        // todo
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
    }
}
