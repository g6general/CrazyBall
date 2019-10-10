using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : HeroBase
{
    public override void Init()
    {
        mMoveType = eMoveType.NO_MOVE;
        mStartPoint = GameObject.Find("StartPoint").GetComponent<Transform>();
    }

    public override void Move()
    {
        Debug.Log("Move Ball!");
    }
    
    public override void Break()
    {
        Debug.Log("Break Ball!");
    }

    public override void PlaceToStart()
    {
        // todo
    }
}
