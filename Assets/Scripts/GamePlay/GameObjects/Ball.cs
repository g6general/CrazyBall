using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : HeroBase
{
    public override void Init()
    {
        Debug.Log("Init Ball!");
    }

    public override void Move()
    {
        Debug.Log("Move Ball!");
    }
    
    public override void Break()
    {
        Debug.Log("Break Ball!");
    }
}
