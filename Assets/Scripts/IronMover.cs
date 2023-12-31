using UnityEngine;
public class IronMover : MonoBehaviour {
    public float moveSpeed = 5f;

    void Update() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}