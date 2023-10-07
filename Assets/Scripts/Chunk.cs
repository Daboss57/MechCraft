using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public bool filled = false;
    public int[] coords;
    // ids of tiles, used for tilemap display
    public int[,] chunkTiles;
    // other tile data
    public GenericTile[,] chunkData;

    public Chunk(int x, int y,int chunkSize)
    {
        coords = new int[2] {x, y};
        chunkTiles = new int[chunkSize, chunkSize];
        chunkData = new GenericTile[chunkSize, chunkSize];
    }

}
