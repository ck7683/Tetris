using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public struct BlockInfo {
        public GameObject[] blocks;
        public int[,] blockShape;
        public int x, y;
        public int type;
    };
    public BlockInfo curBlocks;
    int x, y;
    float sx, sy;
    int[,] ary;
    GameObject[,] ary2;
    enum Color{
        red,
        orange,
        yellow,
        green,
        blue,
        cyan,
        pink
    }

    public void InitSetting(int Xsize, int Ysize, float scaleX, float scaleY)
    {
        x = Xsize; sx = scaleX;
        y = Ysize; sy = scaleY;
        ary = new int[y+10, x+5];
        ary2 = new GameObject[y + 10, x + 5];
        for (int i = 0; i < y + 10; i++)
            ary[i, 0] = ary[i, x + 1] = 1;
        for (int j = 0; j <= x + 1; j++)
            ary[1, j] = 1;
    }

    GameObject GetBlock(Color color = Color.red)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Block"), transform);
        string colorName;
        switch (color)
        {
            case Color.red: colorName = "Red"; break;
            case Color.orange: colorName = "Orange"; break;
            case Color.yellow: colorName = "Yellow"; break;
            case Color.green: colorName = "Green"; break;
            case Color.blue: colorName = "Blue"; break;
            case Color.cyan: colorName = "Cyan"; break;
            case Color.pink: colorName = "Pink"; break;
            default: colorName = "Red"; break;
        }
        string path = "Material/" + colorName;
        Material material = Instantiate(Resources.Load<Material>(path));
        go.GetComponent<MeshRenderer>().material = material;
        go.transform.localScale = new Vector3(sx, sy, 0.1f);
        return go;
    }

    /// <summary>
    /// 블록 4개 모양
    /// </summary>
    /// <param name="type">
    /// 0 ~ 7 (0:random)
    /// </param>
    public void SpawnBlocks(int type = 0)
    {
        if (type <= 0 || type > 7)
            type = Random.Range(1,8);
        curBlocks.type = type;
        curBlocks.blocks = new GameObject[4];
        curBlocks.x = x / 2;
        curBlocks.y = y + 2;
        for(int i = 0; i < 4; i++)
            curBlocks.blocks[i] = GetBlock((Color)(type - 1));
        switch (type)
        {
            case 1:
                curBlocks.blockShape = new int[4, 4]{
                    { 0, 0, 0, 0},
                    { 1, 1, 0, 0},
                    { 0, 1, 0, 0},
                    { 0, 1, 0, 0}
                };
                break;
            case 2:
                curBlocks.blockShape = new int[4, 4]{
                    { 0, 0, 0, 0},
                    { 0, 1, 1, 0},
                    { 0, 1, 0, 0},
                    { 0, 1, 0, 0}
                };
                break;
            case 3:
                curBlocks.blockShape = new int[4, 4]{
                    { 0, 0, 0, 0},
                    { 0, 0, 0, 0},
                    { 1, 1, 1, 0},
                    { 0, 1, 0, 0}
                };
                break;
            case 4:
                curBlocks.blockShape = new int[4, 4]{
                    { 0, 0, 0, 0},
                    { 0, 0, 0, 0},
                    { 1, 1, 0, 0},
                    { 0, 1, 1, 0}
                };
                break;
            case 5:
                curBlocks.blockShape = new int[4, 4]{
                    { 0, 0, 0, 0},
                    { 0, 0, 0, 0},
                    { 0, 1, 1, 0},
                    { 1, 1, 0, 0}
                };
                break;
            case 6:
                curBlocks.blockShape = new int[4, 4]{
                    { 0, 1, 0, 0},
                    { 0, 1, 0, 0},
                    { 0, 1, 0, 0},
                    { 0, 1, 0, 0}
                };
                break;
            case 7:
                curBlocks.blockShape = new int[4, 4]{
                    { 0, 0, 0, 0},
                    { 0, 0, 0, 0},
                    { 1, 1, 0, 0},
                    { 1, 1, 0, 0}
                };
                break;
        }
    }

    void Swap(ref int a, ref int b)
    {
        int t = a; a = b; b = t;
    }

    public bool CheckCollision(int curX, int curY, int[,] blocks)
    {
        bool chk = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (curY + i < 0 || curX + j < 0 || curX + j > x+4)
                    continue;
                if ((ary[curY + i, curX + j] & blocks[3 - i, j]) == 1)
                    chk = true;
            }
        }
        return chk;
    }

    public int[,] CopyShape(int[,] source)
    {
        int[,] blockShape = new int[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                blockShape[i, j] = source[i, j];
        return blockShape;
    }

    public int[,] TransBlock(int[,] blockShape, int type)
    {
        switch (type)
        {
            case 1:
            case 2:
            case 3:
                int t = blockShape[2, 0];
                blockShape[2, 0] = blockShape[1, 1];
                blockShape[1, 1] = blockShape[2, 2];
                blockShape[2, 2] = blockShape[3, 1];
                blockShape[3, 1] = t;

                t = blockShape[1, 0];
                blockShape[1, 0] = blockShape[1, 2];
                blockShape[1, 2] = blockShape[3, 2];
                blockShape[3, 2] = blockShape[3, 0];
                blockShape[3, 0] = t;
                break;
            case 4:
                Swap(ref blockShape[3, 1], ref blockShape[3, 0]);
                Swap(ref blockShape[3, 2], ref blockShape[1, 1]);
                break;
            case 5:
                Swap(ref blockShape[1, 1], ref blockShape[3, 0]);
                Swap(ref blockShape[3, 1], ref blockShape[3, 2]);
                break;
            case 6:
                for(int i = 0; i < 4; i++)
                    Swap(ref blockShape[2, i], ref blockShape[3-i, 1]);
                break;
            case 7:
                break;
        }
        return blockShape;
    }

    void Start()
    {
    }

    public float passedTime;
    public float limitTime;
    void DeleteLine()
    {
        for(int i = y+1; i > 1; i--)
        {
            int cnt = 0;
            for(int j = 1; j <= x; j++)
            {
                cnt += ary[i, j];
            }
            if(cnt == x)
            {
                for (int j = 1; j <= x; j++)
                {
                    ary[i, j] = 0;
                    Destroy(ary2[i, j]);
                }
                for (int ii = i; ii <= y+2; ii++)
                {
                    for(int j = 1; j <= x; j++)
                    {
                        ary[ii, j] = ary[ii + 1, j];
                        ary2[ii, j] = ary2[ii + 1, j];
                    }
                }
                for (int j = 1; j <= x; j++)
                {
                    ary[y + 1, j] = 0;
                    ary2[y + 1, j] = null;
                }
            }
        }
    }
    void Update()
    {
        
        passedTime += Time.deltaTime;
        if(passedTime >= limitTime)
        {
            passedTime -= limitTime;
            if (CheckCollision(curBlocks.x, curBlocks.y - 1, CopyShape(curBlocks.blockShape)) )
            {
                int idx = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        ary[curBlocks.y + i, curBlocks.x + j] |= curBlocks.blockShape[3 - i, j];
                        if (curBlocks.blockShape[3 - i, j] == 1)
                            ary2[curBlocks.y + i, curBlocks.x + j] = curBlocks.blocks[idx++];
                    }
                }
                SpawnBlocks(0);
                passedTime = 0;
                DeleteLine();
            }
            else
                curBlocks.y--;
        }
        int t = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (curBlocks.blockShape[3 - i, j] == 1)
                {
                    curBlocks.blocks[t++].transform.position = new Vector3(curBlocks.x + j - x / 2, curBlocks.y + i - y / 2, 0);
                }
            }
        }
        for(int i = 2; i <= y +1; i++)
        {
            for(int j = 1; j <= x; j++)
            {
                if(ary2[i,j])
                    ary2[i, j].transform.position = new Vector3(j - x/2, i - y/2, 0);
            }
        }
    }
}
