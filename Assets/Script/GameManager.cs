using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BlockManager blockManager;
    Border border;
    void Start()
    {
        blockManager = transform.Find("BlockManager").GetComponent<BlockManager>();
        border = transform.Find("Border").GetComponent<Border>();

        border.InitSetting(10, 20);
        blockManager.InitSetting(10, 20, 0.9f, 0.9f);
        blockManager.SpawnBlocks(0);
    }

    float coolTime;
    float limitTime = 0.1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            int[,] copy = blockManager.CopyShape(blockManager.curBlocks.blockShape);
            if (!blockManager.CheckCollision(blockManager.curBlocks.x, blockManager.curBlocks.y,
                copy = blockManager.TransBlock(copy, blockManager.curBlocks.type)))
            {
                blockManager.curBlocks.blockShape = copy;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (coolTime < 0)
            {
                if (!blockManager.CheckCollision(blockManager.curBlocks.x - 1, blockManager.curBlocks.y, blockManager.curBlocks.blockShape))
                {
                    blockManager.curBlocks.x--;
                    coolTime = limitTime;
                }
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (coolTime < 0)
            {
                if (!blockManager.CheckCollision(blockManager.curBlocks.x + 1, blockManager.curBlocks.y, blockManager.curBlocks.blockShape))
                {
                    blockManager.curBlocks.x++;
                    coolTime = limitTime;
                }
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            blockManager.passedTime += Time.deltaTime*30;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            blockManager.passedTime += 99999999;
        }
        coolTime -= Time.deltaTime;
    }
}
