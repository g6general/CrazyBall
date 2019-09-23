using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroBase : MonoBehaviour
{
    public abstract void Init();
    public abstract void MoveForward();
    public abstract void MoveInPlace();
    public abstract void Fire();
    public abstract void Break();
}
