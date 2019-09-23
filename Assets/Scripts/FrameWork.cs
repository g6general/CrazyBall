using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameWork : MonoBehaviour
{
    private FileManager mFileManager;
    private LogManager mLogManager;
    private NetManager mNetManager;
    private ResourceManager mResourceManager;
    private SoundManager mSoundManager;

    private Parameters mParameters;
    private GamePlay mGamePlay;
    
    void init()
    {
        mFileManager = new FileManager();
        mLogManager = new LogManager();
        mNetManager = new NetManager();
        mResourceManager = new ResourceManager();
        mSoundManager = new SoundManager();
        
        mParameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        mGamePlay = new GamePlay();
    }
    
    void Start()
    {
        init();
    }

    void Update()
    {
        mGamePlay.Process();
    }
}
