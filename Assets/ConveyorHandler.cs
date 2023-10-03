using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Resource"))
        {
            // Move the object horizontally
            other.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
}