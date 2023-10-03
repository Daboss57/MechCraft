using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class GridHandler : MonoBehaviour
{
    // every tile
    int chunkSize = 32;

    // list of all tiles in a chunk
    public List<int> chunkTiles = new List<int>();

    public List<GenericTile> chunkData = new List<GenericTile>();


    [SerializeField]
    public GameObject world = null;

    public Tilemap tilemap = null;
    public List<TileBase> tilebases = new List<TileBase>();


    // Start is called start because it starts
    void Start()
    {
        tilemap = world.GetComponent<Tilemap>();

        CreateChunk(chunkTiles);
        print(tilemap);
        print(chunkTiles[3]);
    }

    void CreateChunk(List<int> list) {
        for (int i = 0; i < 1024; i++)
        {
            //Level loading stuff will go here
            list.Add(0);
            GenericTile a = new Conveyor();
            a.power = 5;
            chunkData.Add(a);
            
        }
        // yay it works
        print(chunkData[4]);
        print(chunkData[5].power);
        
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
                    UpdateTile(mousePos.x, mousePos.y, 1);
                }
            }
        else if (Input.GetMouseButtonDown(1))
            {
                Vector3 mousePos = Input.mousePosition;
                {
                    
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Debug.Log(mousePos.x);
                    Debug.Log(mousePos.y);
                    UpdateTile(mousePos.x, mousePos.y, 0);
                }
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

    void UpdateTile(float x, float y, int tileValue)
    {
        chunkTiles[Coords2Index(x, y)] = tileValue;
        tilemap.SetTile(new Vector3Int((int)Math.Floor(x), (int)Math.Floor(y), 0), tilebases[tileValue]);
        print("updated");
    }

}
