using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    private float mBlockSizeX;
    private float mBlockSizeY;
    private float mBlockSizeZ;

    private float mDeltaZ;

    private int mBlocksInLength;
    private int mBlocksInHeight;

    private Color mRigidBlockColor;
    private Color mSoftBlockColor;

    private List<Position> mBarriers;
    private List<List<GameObject>> mBlocks;

    void Start()
    {
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        mBlockSizeX = parameters.mBlockSizeX;
        mBlockSizeY = parameters.mBlockSizeY;
        mBlockSizeZ = parameters.mBlockSizeZ;
        mDeltaZ = parameters.mDeltaZ;
        mBlocksInLength = parameters.getLength();
        mBlocksInHeight = parameters.getHeight();
        mBarriers = parameters.mBarriers;
        mRigidBlockColor = parameters.mRigidBlockColor;
        mSoftBlockColor = parameters.mSoftBlockColor;
        
        mBlocks = new List<List<GameObject>>();

        CreateWall();
    }

    private void init()
    {
        for (var i = 0; i < mBlocksInHeight; ++i)
        {
            mBlocks.Add(new List<GameObject>(mBlocksInLength));
        }
    }

    public void CreateWall()
    {
        var rigidBlockPref = (GameObject)Resources.Load("Prefabs/rigid_block", typeof(GameObject));
        var softBlockPref = (GameObject)Resources.Load("Prefabs/soft_block", typeof(GameObject));

        rigidBlockPref.transform.localScale = new Vector3(mBlockSizeX, mBlockSizeY, mBlockSizeZ);
        softBlockPref.transform.localScale = new Vector3(mBlockSizeX, mBlockSizeY, mBlockSizeZ);

        var blockMat = (Material)Resources.Load("Materials/block", typeof(Material));
        var breakableMat = (Material)Resources.Load("Materials/breakable", typeof(Material));

        blockMat.color = mRigidBlockColor;
        breakableMat.color = mSoftBlockColor;

        rigidBlockPref.GetComponent<Renderer>().material = blockMat;
        softBlockPref.GetComponent<Renderer>().material = breakableMat;
        
        while (DestroyUpperRow());

        for (var i = 0; i < mBlocksInHeight; ++i)
        {
            mBlocks.Add(new List<GameObject>(mBlocksInLength));
        }

        for (var i = 0; i < mBlocksInHeight; ++i)
        {
            for (var j = 0; j < mBlocksInLength; ++j)
            {
                var vector = new Vector3(0, i * mBlockSizeY, j * mBlockSizeZ + mDeltaZ * (mBlocksInHeight - 1 - i));
                var pos = new Position(i, j);

                if (mBarriers.Contains(pos))
                {
                    var newObject = GameObject.Instantiate(rigidBlockPref, vector, Quaternion.identity);
                    newObject.name = "rigid_block";
                    mBlocks[i].Add(newObject);
                }
                else
                {
                    var newObject = GameObject.Instantiate(softBlockPref, vector, Quaternion.identity);
                    newObject.name = "soft_block";
                    mBlocks[i].Add(newObject);
                }
            }
        }
    }

    public bool DestroyUpperRow()
    {
        if (mBlocks.Count == 0)
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

public struct Position
{
    public int mVertical;
    public int mHorizontal;

    public Position(int vertical, int horizontal)
    {
        mVertical = vertical;
        mHorizontal = horizontal;
    }
}
