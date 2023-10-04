using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerHandler : MonoBehaviour
{
    public float productionRate = 10.0f; // Amount of iron produced per second
    public GameObject ironPrefab;

    private float nextProductionTime = 0.0f;

    void Update()
    {
        nextProductionTime += 0.0001f;

        if(nextProductionTime >= productionRate) {
            ProduceIron();
            nextProductionTime = 0.0f;
        }
    }

    void ProduceIron()
    {
        // Add code here to generate iron
        // For example, you might increment a resource counter or instantiate an iron object.
        ResourceManager.Instance.AddIron(1);

        Vector3 spawnPosition = transform.position + transform.right * 1.0f;

        GameObject iron = Instantiate(ironPrefab, spawnPosition, Quaternion.identity);

        iron.transform.SetParent(transform);
    }
}
