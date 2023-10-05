using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LevelHandler : MonoBehaviour
{
    public int chunkSize = 32;
    public int worldSize = 16;
    public string worldName = "test";

    public class Save
    {
        public string name;
        public int time = 0;
        public Chunk[,] chunks;
        
    }

    public TextAsset saveThing;
    public bool readFromAppdata = false;

    // public Save json = new Save();
    public Save world = new Save();
    //file name of save in game appdata
    public string saveFile = "save.json";

    string savePath;

    
    // Start is called before the first frame update
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, saveFile);
        // json = JsonUtility.FromJson<Save>(raw.text);

        world = LoadWorld();

        //probably redundant
        if (world.chunks != null)
        {
            world = CreateWorld(worldName, worldSize, chunkSize);
        }



    }

    //This should probably be in the Save class but whatever
    Save CreateWorld(string name, int worldSize, int chunkSize) {
        Save save = new Save();
        Chunk[,] chunks = new Chunk[worldSize, worldSize];

        for (int i = 0; i < Math.Pow(worldSize, 2); i++)
        {
            int x = (int)Math.Floor((float)i / worldSize);
            int y = (int)Math.Floor((float)i / worldSize);

            Chunk chunk = new Chunk(x, y, chunkSize);
            chunks[x, y] = chunk;
            
        }
        save.name = name;
        save.chunks = chunks;
        // yay it works
        // print(chunkData[4]);
        // print(chunkData[5].power);
        return save;
    }

    public Save LoadWorld()
    {
        // load world saveFile from SavePath
        // create world if it doesn't exist
        if (readFromAppdata)
        {
            savePath = Path.Combine(Application.persistentDataPath, saveFile);
            File.Create(savePath).Dispose();

            string saveRaw = File.ReadAllText(savePath);

            Save world = JsonUtility.FromJson<Save>(saveRaw);
        } else
        {
            Save world = JsonUtility.FromJson<Save>(saveThing.text);
        }

        if (world == null)
        {
            world = CreateWorld(worldName, worldSize, chunkSize);
        }
        // print(world);

        return world;

    }

    public void SaveWorld()
    {
        //save world to SaveFile in SavePath
        savePath = Path.Combine(Application.persistentDataPath, saveFile);
        string data = JsonUtility.ToJson(world);
        File.WriteAllText(savePath, data);
        print("World Saved to " + savePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
