using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay
{
    private FileManager mFileManager;
    private LogManager mLogManager;
    private NetManager mNetManager;
    private ResourceManager mResourceManager;
    private SoundManager mSoundManager;
    
    private Parameters mParameters;

    public void SetFileManager(FileManager manager) { mFileManager = manager; }
    public void SetLogManager(LogManager manager) { mLogManager = manager; }
    public void SetNetManager(NetManager manager) { mNetManager = manager; }
    public void SetResourceManager(ResourceManager manager) { mResourceManager = manager; }
    public void SetSoundManager(SoundManager manager) { mSoundManager = manager; }
    public void SetParameters(Parameters parameters) { mParameters = parameters; }



    public void Process()
    {
        Debug.Log("Inside gameplay");
    }
}
