using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//each tile will store data in one of these
public class GenericTile
{
    public int[] connections = new int[4];
    public int power = 0;
    public Dictionary<int, int> storage = new Dictionary<int, int>();

}

public class Conveyor : GenericTile
{

}
