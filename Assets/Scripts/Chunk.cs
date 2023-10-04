using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public int[] coords;
    public int[,] chunkTiles;
    public GenericTile[,] chunkData;

    public Chunk(int x, int y,int chunkSize)
    {
        coords = new int[2] {x, y};
        chunkTiles = new int[chunkSize, chunkSize];
    }

}
