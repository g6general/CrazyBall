using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    protected GamePlay.GameData mGameData;
    
    public delegate void GameEventHandlerDelegate(GameEvent e);
    public abstract void Init(GamePlay.GameData gameData);

    public abstract void UpdateObject();

    public void UpdateGameData(GamePlay.GameData gameData)
    {
        mGameData = gameData;
    }
}
