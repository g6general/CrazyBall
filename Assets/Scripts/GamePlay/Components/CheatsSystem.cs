#define CHEATS_ACTIVATED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
