using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{

    public static Vector3Int gridSize = new Vector3Int(8, 17, 8);
    public GameObject Quad;

    public static Transform[,,] Grid = new Transform[gridSize.y, gridSize.x, gridSize.z];

    void Start()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.z; z++)
            {
                GameObject newQuad = Instantiate(Quad, new Vector3(x, -0.5f, z), Quaternion.Euler(90f, 0f, 0f));
                newQuad.transform.parent = this.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 d = new Vector3(gridSize.x, gridSize.y, gridSize.z);

        Vector3 center = new Vector3(transform.position.x + gridSize.x * 0.5f, transform.position.y + gridSize.y * 0.5f, transform.position.z + gridSize.z * 0.5f);

        Gizmos.DrawWireCube(center, d * 1);

        for (int y = 0; y < WorldGrid.gridSize.y; ++y)
        {
            for (int x = 0; x < WorldGrid.gridSize.x; ++x)
            {
                for (int z = 0; z < WorldGrid.gridSize.z; ++z)
                {
                    if (WorldGrid.Grid[y, x, z] != null)
                    {
                        Gizmos.DrawWireCube(new Vector3Int(x,y,z), new Vector3(1,1,1));
                    }
                }
            }
        }
    }



    public static bool IsInGrid(Vector3 pos)
    {
        return ((int)pos.y >= 0 &&
                (int)pos.x >= 0 && (int)pos.x < gridSize.x &&
                (int)pos.z >= 0 && (int)pos.z < gridSize.z);
    }

    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < gridSize.x; ++x)
        {
            for (int z = 0; z < gridSize.z; ++z)
            {
                if (Grid[y, x, z] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void DeleteRow(int y)
    {
        for (int x = 0; x < gridSize.x; ++x)
        {
            for (int z = 0; z < gridSize.z; ++z)
            {
                GameObject.Destroy(Grid[y, x, z].gameObject);
                Grid[y, x, z] = null;
            }
        }
    }

    public static void DecreseRow(int y)
    {
        //if gridposition is occupied, move downwards 
        for (int x = 0; x < gridSize.x; ++x)
        {
            for (int z = 0; z < gridSize.z; ++z)
            {
                if(Grid[y, x, z] != null)
                {
                    Grid[y - 1, x, z] = Grid[y, x, z];
                    Grid[y, x, z] = null;
                    Grid[y - 1, x, z].position += new Vector3Int(0, -1, 0);
                }
            }
        }
    }

    public static void DecreaseRowsAbove(int y)
    {
        for(int i = y;i<gridSize.y;++i)
        {
            DecreseRow(i);
        }
    }

    public static void DeleteWholeRows()
    {
        //get all full rows

        int fullRows = 0;

        for (int y = 0; y < gridSize.y; y++)
        {
            if (IsRowFull(y))
            {
                fullRows++;
            }
        }


        //destroy all full rows

        for (int i = 0; i < fullRows; i++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (IsRowFull(y))
                {
                    DeleteRow(y);


                    GameManager.gmInstance.AddPoints(fullRows);

                    DecreaseRowsAbove(y + 1);
                }
            }
        }
    }

    public static Vector3 RoundVector(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }
}