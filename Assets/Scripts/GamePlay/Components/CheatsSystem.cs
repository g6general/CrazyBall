#define CHEATS_ACTIVATED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class CheatsSystem
{
    private bool mIsCheatsActivated;
    
    public CheatsSystem()
    {
#if CHEATS_ACTIVATED
        mIsCheatsActivated = true;
#else
        mIsCheatsActivated = false;
#endif
        // todo
    }
    
    // todo
}
