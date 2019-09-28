using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroBase : MonoBehaviour
{
    public enum eMoveType
    {
        FORWARD,
        INPLACE,
        FIRE
    };

    protected eMoveType mMoveType;
    
    protected Parameters mParameters;
    
    public abstract void Init();
    public abstract void Move();
    public abstract void Break();
    public void SetMoveType(eMoveType type) { mMoveType = type; }
}
