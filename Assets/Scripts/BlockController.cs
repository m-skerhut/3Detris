using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockController : MonoBehaviour
{
    float timer;
    bool bMovable = true;

    void Start()
    {

    }

    public bool IsValidGridPosition()
    {
        //check if the 4 cubes of the block are in grid
        foreach (Transform cube in transform)
        {
            Vector3 v = WorldGrid.RoundVector(cube.position);
            if (!WorldGrid.IsInGrid(v))
            {
                return false;
            }

            if (WorldGrid.Grid[(int)v.y, (int)v.x, (int)v.z] != null &&
                WorldGrid.Grid[(int)v.y, (int)v.x, (int)v.z].parent != transform)
            {
                return false; 
            }

        }
        return true;
    }

    void UpdateWorldGrid()
    {
        //clear grid from child cubes
        for(int y = 0; y < WorldGrid.gridSize.y; ++y)
        {
            for(int x = 0; x < WorldGrid.gridSize.x; ++x)
            {
                for(int z = 0; z < WorldGrid.gridSize.z; ++z)
                {
                    if(WorldGrid.Grid[y,x,z] != null)
                    {
                        if(WorldGrid.Grid[y, x, z].parent == transform)
                        {
                            WorldGrid.Grid[y, x, z] = null;
                        }
                    }
                }
            }
        }
        //save new position in grid
        foreach (Transform cube in transform)
        {
            Vector3 v = WorldGrid.RoundVector(cube.position);
            WorldGrid.Grid[(int)v.y, (int)v.x, (int)v.z] = cube;
        }
    }

    void Update()
    {
        if (bMovable)
        {
            //move sideways
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.transform.position += new Vector3Int(-1, 0, 0);
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    gameObject.transform.position += new Vector3Int(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.transform.position += new Vector3Int(1, 0, 0);
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    gameObject.transform.position += new Vector3Int(-1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                gameObject.transform.position += new Vector3Int(0, 0, 1);
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    gameObject.transform.position += new Vector3Int(0, 0, -1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                gameObject.transform.position += new Vector3Int(0, 0, -1);
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    gameObject.transform.position += new Vector3Int(0, 0, 1);
                }
            }

            //rotation
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(new Vector3Int(0, 0, 90), Space.World);
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    transform.Rotate(new Vector3Int(0, 0, -90), Space.World);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(new Vector3Int(90, 0, 0), Space.World);
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    transform.Rotate(new Vector3Int(-90, 0, 0), Space.World);
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(new Vector3Int(0, 90, 0), Space.World);
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    transform.Rotate(new Vector3Int(0, 90, 0), Space.World);
                }
            }


            timer += 1 * Time.deltaTime;
            //Drop
            if (Input.GetKey(KeyCode.Space) && timer >= GameManager.gmInstance.quickDropTime)//quick drop
            {
                gameObject.transform.position += new Vector3Int(0, -1, 0);
                timer = 0;
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    gameObject.transform.position += new Vector3Int(0, 1, 0);

                    WorldGrid.DeleteWholeRows();
                    
                    FindObjectOfType<BlockSpawner>().SpawnRandomBlock();
                    
                    enabled = false;
                    
                }
            }
            else if (timer >= GameManager.gmInstance.dropTime) //normal drop
            {
                gameObject.transform.position += new Vector3Int(0, -1, 0);
                timer = 0;
                if (IsValidGridPosition())
                {
                    UpdateWorldGrid();
                }
                else
                {
                    gameObject.transform.position += new Vector3Int(0, 1, 0);

                    WorldGrid.DeleteWholeRows();
                    FindObjectOfType<BlockSpawner>().SpawnRandomBlock();
                    
                    enabled = false;
                }
            }
        }
    }
}
