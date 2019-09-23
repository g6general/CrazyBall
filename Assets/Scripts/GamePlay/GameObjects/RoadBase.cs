using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoadBase : MonoBehaviour
{
    public abstract void Init();
    public abstract void Build();
    public abstract void Destroy();
    public abstract void DestroyRaw();
}
