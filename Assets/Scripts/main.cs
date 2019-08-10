using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class main : MonoBehaviour
{
    private int mBlockSizeX;
    private int mBlockSizeY;
    private int mBlockSizeZ;

    private int mBlocksInLength;
    private int mBlocksInHeight;
    private List<Position> mBlocks;

    void Start()
    {
        init();
        
        var rigidBlockPref = (GameObject)Resources.Load("Prefabs/rigid_block", typeof(GameObject));
        var softBlockPref = (GameObject)Resources.Load("Prefabs/soft_block", typeof(GameObject));
        
        rigidBlockPref.transform.localScale = new Vector3(mBlockSizeX, mBlockSizeY, mBlockSizeZ);
        softBlockPref.transform.localScale = new Vector3(mBlockSizeX, mBlockSizeY, mBlockSizeZ);

        var blockMat = (Material)Resources.Load("Materials/block", typeof(Material));
        var breakableMat = (Material)Resources.Load("Materials/breakable", typeof(Material));
        
        rigidBlockPref.GetComponent<Renderer>().material = blockMat;
        softBlockPref.GetComponent<Renderer>().material = breakableMat;

        for (var i = 0; i < mBlocksInHeight; ++i)
        {
            for (var j = 0; j < mBlocksInLength; ++j)
            {
                var vector = new Vector3(0, i * mBlockSizeY, j * mBlockSizeZ);
                var pos = new Position(i, j);

                if (mBlocks.Contains(pos))
                    GameObject.Instantiate(rigidBlockPref, vector, Quaternion.identity);
                else
                    GameObject.Instantiate(softBlockPref, vector, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        //var horizontalStep = mHorizontalSpeed * Time.deltaTime;
        //GetComponentInChildren<Camera>().transform.Translate(Vector3.forward * horizontalStep, Space.World);
    }

    private void init()
    {
        mBlockSizeX = 3;
        mBlockSizeY = 1;
        mBlockSizeZ = 10;
        
        mBlocksInLength = 9;
        mBlocksInHeight = 3;

        mBlocks = new List<Position>
        {
            new Position(1, 0),
            new Position(0, 1),
            new Position(2, 1),
            new Position(1, 2),
            new Position(0, 3),
            new Position(2, 3),
            new Position(1, 4),
            new Position(0, 5),
            new Position(2, 5),
            new Position(1, 6),
            new Position(0, 7),
            new Position(2, 7),
            new Position(1, 8)
        };
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