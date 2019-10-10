using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameWork : MonoBehaviour
{
    public static FileManager mFileManager;
    public static TextManager mTextManager;
    public static LogManager mLogManager;
    public static NetManager mNetManager;
    public static ResourceManager mResourceManager;
    public static SoundManager mSoundManager;
    public static VibrationManager mVibrationManager;
    public static StoreManager mStoreManager;

    public static string mGemeVersion;
    public static uint mGameId;
    
    private GamePlay mGamePlay;
    
    void Init()
    {
        mGemeVersion = "1.0.0";
        mGameId = 1;
        
        mFileManager = new FileManager();
        mTextManager = new TextManager();
        mLogManager = new LogManager();
        mNetManager = new NetManager();
        mResourceManager = new ResourceManager();
        mSoundManager = new SoundManager();
        mVibrationManager = new VibrationManager();
        mStoreManager = new StoreManager();
        
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        mGamePlay = new GamePlay(parameters);
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
