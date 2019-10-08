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
        DEBRIEFING_SCREEN,
        SETTINGS_SCREEN,
        SHOP_SCREEN,
        AD_SCREEN
    }

    private eMode mScreenMode;

    public UiSystem()
    {
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
