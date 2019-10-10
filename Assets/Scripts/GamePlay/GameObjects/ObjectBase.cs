using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    public delegate void GameEventHandlerDelegate(GameEvent e);
    public abstract void Init();
}
