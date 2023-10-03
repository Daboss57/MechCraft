using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TheGame : MonoBehaviour
{
    // every tile
    int chunkSize = 32;

    // list of all tiles in a chunk
    public List<int> chunkTiles = new List<int>();

    public List<int> chunkData = new List<int>();


    [SerializeField]
    public GameObject world = null;

    public Tilemap tilemap = null;
    public List<TileBase> tilebases = new List<TileBase>();


    public class Conveyor
    {
        public int[] connections = new int[5];
        public int power = 0;
        public Dictionary<int, int> storage = new Dictionary<int, int>();
    }

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
        // This looks useless right now but it will be useful if we add a chunk system
        // Currently doesn't work with negative numbers
        return ( (int)Math.Floor(y / (chunkSize)) + ((int)x % (chunkSize)) );
    }

    void UpdateTile(float x, float y, int tileValue)
    {
        chunkTiles[Coords2Index(x, y)] = tileValue;
        tilemap.SetTile(new Vector3Int((int)Math.Floor(x), (int)Math.Floor(y), 0), tilebases[tileValue]);
        print("updated");
    }

}
