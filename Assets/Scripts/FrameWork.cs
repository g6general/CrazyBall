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
    private VibrationManager mVibrationManager;

    private GamePlay mGamePlay;
    
    void Init()
    {
        mFileManager = new FileManager();
        mLogManager = new LogManager();
        mNetManager = new NetManager();
        mResourceManager = new ResourceManager();
        mSoundManager = new SoundManager();
        mVibrationManager = new VibrationManager();
        
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        mGamePlay = new GamePlay(parameters);
        
        // geme version
        // game id
    }

    void Awake()
    {
        Init();
        mGamePlay.BeforeBeginSession();
    }

    void Start()
    {
        mGamePlay.BeginSession();
    }

    void Update()
    {
        mGamePlay.Session();
    }

    void OnApplicationQuit()
    {
        mGamePlay.EndSession();
    }
}
