using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class GridHandler : MonoBehaviour
{
    // every tile
    int chunkSize = 32;

    public List<int> tiles = new List<int>();


    [SerializeField]
    public GameObject world = null;

    public Tilemap tilemap = null;
    public List<TileBase> tilebases = new List<TileBase>();

    // Start is called start because it starts
    void Start()
    {
        tilemap = world.GetComponent<Tilemap>();

        CreateChunk(tiles);
        print(tilemap);
        print(tiles[3]);
    }

    void CreateChunk(List<int> list) {
        for (int i = 0; i < 1024; i++)
        {
            //Level loading stuff will go here
            list.Add(0);
        }
        
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
                    UpdateTile(mousePos.x, mousePos.y, 2);
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
        int chunkX = (int)Math.Floor(x / chunkSize);
        int chunkY = (int)Math.Floor(y / chunkSize);
        int indexInChunk = (Math.Abs((int)x) % chunkSize) + (Math.Abs((int)y) % chunkSize) * chunkSize;

        return chunkSize * chunkSize * chunkX + chunkSize * chunkY + indexInChunk;
    }




    void UpdateTile(float x, float y, int tileValue)
    {
        tiles[Coords2Index(x, y)] = tileValue;
        tilemap.SetTile(new Vector3Int((int)Math.Floor(x), (int)Math.Floor(y), 0), tilebases[tileValue]);
        print("updated");
    }

}
