using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System;

public class GridHandler : MonoBehaviour
{
    // gonna move these two to a singleton later
    public int chunkSize = 32;
    public int worldSize = 16;

    // list of all tiles in a chunk
    public Chunk[,] worldData = null;


    public GameObject tilemapObject = null;
    public GameObject levelHandlerObject = null;
    LevelHandler levelHandler = null;

    Tilemap tilemap = null;
    public List<TileBase> tilebases = new List<TileBase>();

    public bool causeLotsOfLag = false;

    // Start is called start because it starts
    void Start()
    {
        tilemap = tilemapObject.GetComponent<Tilemap>();
        print(tilemap);
        tilemap.SetTile(new Vector3Int(12, 6, 0), tilebases[1]);

        levelHandler = levelHandlerObject.GetComponent<LevelHandler>();
        print(levelHandler);

        tilebases = CreateTiles();

        SetWorldData();

        if (causeLotsOfLag == true)
        {
            UpdateEveryChunkInTheWorldToSetTheCorrectStateForThePropertyOfBeingFilledBecauseNotEveryChunkHasAnAccurateValue();
            FindConveyorLinesByScanningTheEntireWorldExceptForTheChunksThatAreSupposedlyEmpty();
        }
        // print(chunkTiles[3]);
        // print(Math.Pow(4,3));
    }
    
    List<TileBase> CreateTiles()
    {
        List<TileBase> tiles = new List<TileBase>();
        string[] paths = Directory.GetFiles(Path.Combine(Application.dataPath, "Tiles"), "*.png");

        tiles.Add(null);

        for (int i = 0; i < paths.Length; i++)
        {
            Tile tile = Tile.CreateInstance<Tile>();
            Texture2D texture = new Texture2D(1,1);
            
            texture.LoadImage(File.ReadAllBytes(paths[i]));
            texture.filterMode = FilterMode.Point;
            tile.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16.0f);

            tiles.Add(tile);
            print(paths[i]);
        }

        return tiles;
    }

    void SetWorldData()
    {
        worldData = levelHandler.world.chunks;
        print(levelHandler);
        print(levelHandler.world);
        print(levelHandler.world.chunks);
        for (int cx = 0; cx < worldSize; cx++)
        {
            for (int cy = 0; cy < worldSize; cy++)
            {
                for (int tx = 0; tx < chunkSize; tx++)
                {
                    for (int ty = 0; ty < chunkSize; ty++)
                    {
                        // print(worldData);
                        // tilemap.SetTile(new Vector3Int(12, 6, 0), tilebases[1]);
                        tilemap.SetTile(new Vector3Int(cx * chunkSize + tx, cy * chunkSize + ty, 0), tilebases[worldData[cx, cy].chunkTiles[tx, ty]]);
                        // tilemap.SetTile(new Vector3Int(
                            // cx * chunkSize + tx, cy * chunkSize + ty, 0), 
                            // tilebases[worldData[cx, cy].chunkTiles[tx, ty]]
                            // );
                    }
                }
            }
        }
    }

    // world loading and saving is in LevelHandler
    // Chunk[,] CreateWorld() {
    //     Chunk[,] chunks = new Chunk[worldSize, worldSize];

    //     for (int i = 0; i < Math.Pow(worldSize, 2); i++)
    //     {
    //         //Level loading stuff will go here
    //         int x = (int)Math.Floor((float)i / worldSize);
    //         int y = (int)Math.Floor((float)i / worldSize);

    //         Chunk chunk = new Chunk(x, y, chunkSize);
    //         // worldData[x, y] = chunk;
    //         chunks[x, y] = chunk;
            
    //     }
    //     // yay it works
    //     // print(chunkData[4]);
    //     // print(chunkData[5].power);
    //     return chunks;
    // }

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
        else if (Input.GetKeyDown("t"))
        {
            levelHandler.world.chunks = worldData;
            levelHandler.SaveWorld();
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

        // print(worldData[1,1]);
        // Chunk chunk = worldData[chunkPos[0], chunkPos[1]];
        // chunkTiles[localPos] = tileValue;
        
        worldData[chunkPos[0], chunkPos[1]].chunkTiles[localPos[0], localPos[1]] = tileValue;
        if (tileValue != 0)
        {
            worldData[chunkPos[0], chunkPos[1]].filled = true;
        } else 
        {
            worldData[chunkPos[0], chunkPos[1]].filled = false;
        }

        tilemap.SetTile(new Vector3Int(x, y, 0), tilebases[tileValue]);
        print("updated");
    }

    void UpdateEveryChunkInTheWorldToSetTheCorrectStateForThePropertyOfBeingFilledBecauseNotEveryChunkHasAnAccurateValue()
    {
        for (int c = 0; c < worldData.Length; c++) {
            Chunk chunk = worldData[c % worldSize, (int)(c / worldSize)];
            print(new Vector2(c % worldSize, (int)(c / worldSize)));
            for (int i = 0; i < chunk.chunkTiles.Length; i++)
            {
                print(new Vector2(i % chunkSize, (int)(i / chunkSize)));
                if (chunk.chunkTiles[i % chunkSize, (int)(i / chunkSize)] != 0)
                {
                    chunk.filled = true;
                    goto GetMeOutOfHereBeforeThePerformanceDies;
                }
            }

            chunk.filled = false;
            continue;

            GetMeOutOfHereBeforeThePerformanceDies:;
        }
        print(worldData.Length);
        print(worldData[1,1].chunkTiles.Length);

    }

    void FindConveyorLinesByScanningTheEntireWorldExceptForTheChunksThatAreSupposedlyEmpty()
    {
        print("a");
        //              hey look, we're using c++   that means it's efficient
        for (int c = 0; c < worldData.Length; c++) {
            Chunk chunk = worldData[c % worldSize, (int)(c / worldSize)];
            if (chunk.filled == true)
            {
                for (int i = 0; i < chunk.chunkTiles.Length; i++)
                {
                    Vector2Int tile = new Vector2Int(i % chunkSize, (int)(i / chunkSize));
                    int tileValue = chunk.chunkTiles[tile[0], tile[1]];
                    if (tileValue == 4)
                    {
                        print("miner " + tile);

                    }
                }
            }

        }
    }

    List<Vector2Int> SearchConveyorLine(Vector2Int source)
    {
        List<Vector2Int> result = new List<Vector2Int> ();
        // insert code here
        return result;
    }

}
