using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class GridHandler : MonoBehaviour
{
    public int chunkSize = 32;
    public int worldSize = 16;

    // list of all tiles in a chunk
    public Chunk[,] worldData;


    [SerializeField]
    public GameObject world = null;
    public GameObject levelHandler = null;

    public Tilemap tilemap = null;
    public List<TileBase> tilebases = new List<TileBase>();


    // Start is called start because it starts
    void Start()
    {
        tilemap = world.GetComponent<Tilemap>();

        worldData = CreateWorld();
        print(tilemap);
        // print(chunkTiles[3]);
        // print(Math.Pow(4,3));
    }

    Chunk[,] CreateWorld() {
        Chunk[,] chunks = new Chunk[worldSize, worldSize];

        for (int i = 0; i < Math.Pow(worldSize, 2); i++)
        {
            //Level loading stuff will go here
            int x = (int)Math.Floor((float)i / worldSize);
            int y = (int)Math.Floor((float)i / worldSize);

            Chunk chunk = new Chunk(x, y, chunkSize);
            // worldData[x, y] = chunk;
            chunks[x, y] = chunk;
            
        }
        // yay it works
        // print(chunkData[4]);
        // print(chunkData[5].power);
        return chunks;
    }

    // Update is called very many times per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
                UpdateTile((int)Math.Floor(mousePos.x), (int)Math.Floor(mousePos.y), 1);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
                UpdateTile((int)Math.Floor(mousePos.x), (int)Math.Floor(mousePos.y), 0);
            }
        }
        else if (Input.GetKey("t"))
        {
            levelHandler.GetComponent<LevelHandler>().SaveWorld();
        }
    }

    int Coords2Index(float x, float y)
    {
        // coords from 0 to chunkSize - 1
        if (x < 0 || x >= chunkSize || y < 0 || y >= chunkSize )
        {
            return -1;
        }

        int chunkX = (int)Math.Floor(x / chunkSize);
        int chunkY = (int)Math.Floor(y / chunkSize);
        int indexInChunk = (Math.Abs((int)x) % chunkSize) + (Math.Abs((int)y) % chunkSize) * chunkSize;

        print(chunkSize * chunkSize * chunkX + chunkSize * chunkY + indexInChunk);

        return chunkSize * chunkSize * chunkX + chunkSize * chunkY + indexInChunk;
    }

    int[] GlobalPos2Chunk(int x, int y)
    {
        // Math.Floor is being dumb so you have to typecast to a float or double first
        return new int[] {(int)Math.Floor((float)x / chunkSize), (int)Math.Floor((float)y / chunkSize)};
    }

    int[] GlobalPos2LocalPos(int x, int y)
    {
        return new int[] {x % chunkSize, y % chunkSize};
    }

    void UpdateTile(int x, int y, int tileValue)
    {
        int[] chunkPos = GlobalPos2Chunk(x, y);
        int[] localPos = GlobalPos2LocalPos(x, y);

        print(worldData[1,1]);
        Chunk chunk = worldData[chunkPos[0], chunkPos[1]];
        // chunkTiles[localPos] = tileValue;

        tilemap.SetTile(new Vector3Int(x, y, 0), tilebases[tileValue]);
        print("updated");
    }

}
