using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroBase : ObjectBase
{
    public enum eMoveType
    {
        FORWARD,
        INPLACE,
        FIRE,
        NO_MOVE
    };

    protected eMoveType mMoveType;
    
    protected Parameters mParameters;
    
    protected Transform mStartPoint;
    protected Transform mViewPoint;

    public abstract void PlaceToStart();
    public abstract void Move();
    public abstract void Break();
    public void SetMoveType(eMoveType type) { mMoveType = type; }
}
