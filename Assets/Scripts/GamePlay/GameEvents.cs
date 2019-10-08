using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventsList
{
    public enum eType
    {
        // game objects
        GE_WIN,
        GE_DEFEAT,
        GE_TAP_OCCURRED,
        GE_COLLISION_OCCURRED,
        
        // levels
        GE_LEVEL_UP,

        // ui
        GE_GAME_IS_LOADED,
        GE_START_LEVEL_BUTTON,
        GE_NEXT_LEVEL_BUTTON,
        GE_CONTINUE_GAME_BUTTON,
        GE_CLOSE_AD_BUTTON,
        GE_CLOSE_SHOP_BUTTON,
        GE_PURCHASE_1_BUTTON,
        GE_PURCHASE_2_BUTTON,
        GE_PURCHASE_3_BUTTON,
        GE_REMOVE_AD_BUTTON,
        GE_SHOW_AD_FOR_REWARD_BUTTON,
        GE_SHOP_BUTTON,
        GE_SETTINGS_BUTTON,
        GE_BRIEFING_PLAY_BUTTON,
        
        // cheats
        GE_SHOW_ALL_CHEAT_BUTTON,
        GE_REMOVE_ALL_CHEAT_BUTTON,
        GE_WIN_LEVEL_CHEAT_BUTTON,
        GE_SET_LEVEL_CHEAT_BUTTON,
        GE_SWITCH_ON_AD_CHEAT_BUTTON,
        GE_SWITCH_OFF_AD_CHEAT_BUTTON,

        // default  
        GE_NO
    }
}

public struct GameEvent
{
    public GameEventsList.eType mEventType;
    
    public GameEvent(GameEventsList.eType type)
    {
        mEventType = type;
    }
}

public class GameEventSender
{
    public delegate void GameEventHandlerDelegate(GameEvent e);
}

public class GameEventSubscriber
{
    protected GameEventsList.eType mCurrentEvent;

    protected GameEventSubscriber()
    {
        mCurrentEvent = GameEventsList.eType.GE_NO;
    }

    protected void GameEventHandler(GameEvent e)
    {
        mCurrentEvent = e.mEventType;
    }
}
