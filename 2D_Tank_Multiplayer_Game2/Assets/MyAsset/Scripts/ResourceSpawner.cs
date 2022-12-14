using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceSpawner : MonoBehaviour
{
    private Tilemap tilemap;
    private List<Vector3> grassTileWorldPos = new List<Vector3>();
    private int grassTileCount;
    private int resourceCount;
    public List<GameObject> resources = new List<GameObject> ();
    private bool[] grassTileHasEmptySlot;

    //计时器
    public float spawnDelay = 1f;
    private float spawnTimer = 0f;

    public int maxGen = 100;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        Vector3Int tmOrg = tilemap.origin;
        Vector3Int tmSz = tilemap.size;

        for(int x = tmOrg.x; x < tmSz.x; x++)
        {
            for(int y = tmOrg.y; y < tmSz.y; y++)
            {
                if(tilemap.GetTile(new Vector3Int(x,y,0)) != null)
                {
                    Vector3 cellToWorldPos = tilemap.GetCellCenterWorld(new Vector3Int(x,y,0));
                    grassTileWorldPos.Add(cellToWorldPos);
                }
            }
        }

        grassTileCount = grassTileWorldPos.Count;
        resourceCount = resources.Count;

        Debug.Log(grassTileCount);
        Debug.Log(resourceCount);

        //初始化grassTileHasEmptySlot
        grassTileHasEmptySlot = new bool[grassTileCount];
        for(int i = 0; i < grassTileCount; i++)
        {
            grassTileHasEmptySlot[i] = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (count >= maxGen)
            return;

        spawnTimer -= Time.deltaTime;
        if(spawnTimer < 0f)
        {
            //随机选择一个位置
            int aRandomTile = Random.Range(0, grassTileCount);
            if (grassTileHasEmptySlot[aRandomTile])
            {
                Vector3 spawnPos = grassTileWorldPos[aRandomTile];
                //随机选择一个资源
                int aRandomRes = Random.Range(0, resourceCount);
                GameObject spawnRes = resources[aRandomRes];
                Instantiate(spawnRes, spawnPos, Quaternion.identity);
                grassTileHasEmptySlot[aRandomTile] = false;
                count++;

                spawnTimer = spawnDelay;
            }
            
        }

    }
}
