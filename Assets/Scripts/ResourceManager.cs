using UnityEngine;
public class ResourceManager
{
    private static ResourceManager instance;

    public static ResourceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ResourceManager();
            }
            return instance;
        }
    }

    private int ironCount = 0;

    public int IronCount
    {
        get { return ironCount; }
    }

    public void AddIron(int amount)
    {
        ironCount += amount;
        Debug.Log(amount + " iron added. Total iron: " + ironCount);
    }

    public void UseIron(int amount)
    {
        if (amount <= ironCount)
        {
            ironCount -= amount;
            Debug.Log(amount + " iron used. Total iron: " + ironCount);
        }
        else
        {
            Debug.LogWarning("Not enough iron!");
        }
    }
}
