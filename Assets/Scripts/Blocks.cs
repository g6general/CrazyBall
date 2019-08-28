using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    private int mBlockSizeX;
    private int mBlockSizeY;
    private int mBlockSizeZ;

    private int mBlocksInLength;
    private int mBlocksInHeight;
    private List<Position> mBarriers;
    private List<List<GameObject>> mBlocks;

    void Start()
    {
        var parameters = GameObject.Find("MainObject").GetComponent<Parameters>();
        mBlockSizeX = parameters.mBlockSizeX;
        mBlockSizeY = parameters.mBlockSizeY;
        mBlockSizeZ = parameters.mBlockSizeZ;
        mBlocksInLength = parameters.getLength();
        mBlocksInHeight = parameters.getHeight();
        mBarriers = parameters.mBarriers;
        
        var rigidBlockPref = (GameObject)Resources.Load("Prefabs/rigid_block", typeof(GameObject));
        var softBlockPref = (GameObject)Resources.Load("Prefabs/soft_block", typeof(GameObject));

        rigidBlockPref.transform.localScale = new Vector3(mBlockSizeX, mBlockSizeY, mBlockSizeZ);
        softBlockPref.transform.localScale = new Vector3(mBlockSizeX, mBlockSizeY, mBlockSizeZ);

        var blockMat = (Material)Resources.Load("Materials/block", typeof(Material));
        var breakableMat = (Material)Resources.Load("Materials/breakable", typeof(Material));

        blockMat.color = parameters.mRigidBlockColor;
        breakableMat.color = parameters.mSoftBlockColor;

        rigidBlockPref.GetComponent<Renderer>().material = blockMat;
        softBlockPref.GetComponent<Renderer>().material = breakableMat;

        init();

        for (var i = 0; i < mBlocksInHeight; ++i)
        {
            for (var j = 0; j < mBlocksInLength; ++j)
            {
                var vector = new Vector3(0, i * mBlockSizeY, j * mBlockSizeZ);
                var pos = new Position(i, j);

                if (mBarriers.Contains(pos))
                    mBlocks[i].Add(GameObject.Instantiate(rigidBlockPref, vector, Quaternion.identity));
                else
                    mBlocks[i].Add(GameObject.Instantiate(softBlockPref, vector, Quaternion.identity));
            }
        }
    }

    private void init()
    {
        mBlocks = new List<List<GameObject>>();

        for (var i = 0; i < mBlocksInHeight; ++i)
        {
            mBlocks.Add(new List<GameObject>(mBlocksInLength));
        }
    }

    public void DestroyUpperRaw()
    {
        if (mBlocks.Count == 0)
            return;

        var lastIndex = mBlocks.Count - 1;
        var upperRaw = mBlocks[lastIndex];

        foreach (var block in upperRaw)
        {
            Destroy(block);
        }

        mBlocks.RemoveAt(lastIndex);
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