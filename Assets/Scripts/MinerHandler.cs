using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MinerHandler : MonoBehaviour
{
    public float productionRate = 0.1f; // Amount of iron produced per second

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= (1.0f / productionRate))
        {
            ProduceIron();
            timer = 0.0f;
        }
    }

    void ProduceIron()
    {
        // Add code here to generate iron
        // For example, you might increment a resource counter or instantiate an iron object.
        ResourceManager.Instance.AddIron(1);
    }
}
