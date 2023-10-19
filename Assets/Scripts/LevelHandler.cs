using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

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
    public string worldJson = "";
    //file name of save in game appdata
    public string saveFile = "save.json";

    string savePath;

    
    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, saveFile);
        // json = JsonUtility.FromJson<Save>(raw.text);

        world = LoadWorld();
        // print(world.chunks[4,3].chunkTiles[1,2]);
        print(world);
        print(worldJson);
        //probably redundant
        // if (world.chunks == null)
        // {
        //     world = CreateWorld(worldName, worldSize, chunkSize);
        // }



    }

    //This should probably be in the Save class but whatever
    Save CreateWorld(string name, int worldSize, int chunkSize) {
        Save save = new Save();
        Chunk[,] chunks = new Chunk[worldSize, worldSize];

        for (int i = 0; i < Math.Pow(worldSize, 2); i++)
        {
            int x = i % worldSize;
            int y = (int)Math.Floor((float)i / worldSize);

            Chunk chunk = new Chunk(x, y, chunkSize);
            chunks[x, y] = chunk;
            // chunk.chunkTiles[1,2] = 1;
            // print(chunk);
            // print(chunk.chunkTiles[1,2]);
            
        }
        // print(chunks[2,1].chunkTiles[1,2]);

        save.name = name;
        save.chunks = chunks;
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

            // Save world = JsonHelper.FromJson<Save>(saveRaw);
            // Save world = JsonUtility.FromJson<Save>(saveRaw);
            world = JsonConvert.DeserializeObject<Save>(saveRaw);
        } else
        {
            // Save world = JsonHelper.FromJson<Save>(saveThing.text);
            // Save world = JsonUtility.FromJson<Save>(saveThing.text);
            world = JsonConvert.DeserializeObject<Save>(saveThing.text);
        }

        print(JsonConvert.SerializeObject(world));
        if (world == null || world.chunks == null)
        {
            print("no chunks");
            world = CreateWorld(worldName, worldSize, chunkSize);
            worldJson = JsonUtility.ToJson(world);
        }
        print(world.chunks[4,3].chunkTiles[1,2]);
        print(world);
        // print(world);

        return world;

    }

    public void SaveWorld()
    {
        //save world to SaveFile in SavePath
        string data = JsonConvert.SerializeObject(world, Formatting.Indented);
        // string data = JsonUtility.ToJson(world);
        if (readFromAppdata)
        {
            savePath = Path.Combine(Application.persistentDataPath, saveFile);
            File.WriteAllText(savePath, data);
            print("World Saved to " + savePath);
        } else
        {
            File.WriteAllText(Path.Combine(Application.dataPath, saveThing.name + ".json"), data);
            print("World Saved to " + Path.Combine(Application.dataPath, saveThing.name + ".json"));
            print(world);
            print(data);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        // print(world.chunks[7,3].chunkTiles[5,7]);
    }


    // turns out that unity's json library is sort of bad
    // this is from stack overflow because of course it is
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        // public static string ToJson<T>(T[] array, bool prettyPrint)
        // {
        //     Wrapper<T> wrapper = new Wrapper<T>();
        //     wrapper.Items = array;
        //     return JsonUtility.ToJson(wrapper, prettyPrint);
        // }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

}
