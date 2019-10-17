using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Wall : RoadBase
{
    /*
    void Start()    // temp
    {
        Init();
        Build(new Level());
    }
    */

    public override void Init()
    {
        mParameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        
        mRigidBlockPref = (GameObject)Resources.Load("Prefabs/rigid_block", typeof(GameObject));
        mSoftBlockPref = (GameObject)Resources.Load("Prefabs/soft_block", typeof(GameObject));

        mRigidBlockPref.transform.localScale = new Vector3(mParameters.mBlockSizeX, mParameters.mBlockSizeY, mParameters.mBlockSizeZ);
        mSoftBlockPref.transform.localScale = new Vector3(mParameters.mBlockSizeX, mParameters.mBlockSizeY, mParameters.mBlockSizeZ);

        var blockMat = (Material)Resources.Load("Materials/block", typeof(Material));
        var breakableMat = (Material)Resources.Load("Materials/breakable", typeof(Material));

        blockMat.color = mParameters.mRigidBlockColor;
        breakableMat.color = mParameters.mSoftBlockColor;

        mRigidBlockPref.GetComponent<Renderer>().material = blockMat;
        mSoftBlockPref.GetComponent<Renderer>().material = breakableMat;
    }

    public override void Build(Level level)
    {
        Destroy();
        
        mBlocks = new List<List<GameObject>>();
        for (var i = 0; i < level.mBlocksInHeight; ++i)
        {
            mBlocks.Add(new List<GameObject>(level.mBlocksInLength));
        }

        for (var i = 0; i < level.mBlocksInHeight; ++i)
        {
            for (var j = 0; j < level.mBlocksInLength; ++j)
            {
                var vector = new Vector3(0, i * mParameters.mBlockSizeY,
                    j * mParameters.mBlockSizeZ + mParameters.mDeltaZ * (level.mBlocksInHeight - 1 - i));

                var pos = new Position(i, j);

                if (level.mBarriers.Contains(pos))
                {
                    var newObject = GameObject.Instantiate(mRigidBlockPref, vector, Quaternion.identity);
                    newObject.name = "rigid_block";
                    mBlocks[i].Add(newObject);
                }
                else
                {
                    var newObject = GameObject.Instantiate(mSoftBlockPref, vector, Quaternion.identity);
                    newObject.name = "soft_block";
                    mBlocks[i].Add(newObject);
                }
            }
        }
    }

    public override void Destroy()
    {
        while (DestroyUpperRow());
    }

    public override bool DestroyUpperRow()
    {
        if (mBlocks == null || mBlocks.Count == 0)
            return false;

        var lastIndex = mBlocks.Count - 1;
        var upperRaw = mBlocks[lastIndex];

        foreach (var block in upperRaw)
        {
            Destroy(block);
        }

        mBlocks.RemoveAt(lastIndex);

        return true;
    }
}
