using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorHandler : MonoBehaviour
{
    public float speed = 1.0f;

    private BoxCollider2D bc;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Resource"))
        {
            // Check if the resource is on the right side of the conveyor
            if (other.transform.position.x > transform.position.x)
            {
                speed = -1.0f; // Set speed to positive for right movement
            }
            else
            {
                speed = 1.0f; // Set speed to negative for left movement
            }

            other.transform.SetParent(transform, true); // Attach the object to the conveyor
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Resource"))
        {
            other.transform.SetParent(transform); // Keeps it on
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Resource"))
        {
            Transform resourceTransform = other.transform;
            resourceTransform.SetParent(null, true); // Detach the object from the conveyor
        }
    }


    void Update()
    {
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    void Awake() {
        Debug.Log("hi");
        bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.isTrigger = true;
    }
}