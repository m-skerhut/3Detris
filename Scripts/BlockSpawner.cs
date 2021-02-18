using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocks;

    void Start()
    {
        SpawnRandomBlock();
    }

    public void SpawnRandomBlock()
    {
        int rnd = Random.Range(0, blocks.Length);
        GameObject tetrisObj = Instantiate(blocks[rnd], transform.position, Quaternion.identity);
        
        //check if spawn position is available
        if(!tetrisObj.GetComponent<BlockController>().IsValidGridPosition())
        {
            Debug.LogError("Game Over");
            Destroy(tetrisObj);
        }
    }
}
