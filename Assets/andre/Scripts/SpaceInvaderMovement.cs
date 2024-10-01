using UnityEngine;

public class SpaceInvaderMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;       // Horizontal movement speed
    public float dropAmount = 0.5f;      // Amount to move down when reaching the edge
    public float minX = -8.0f;           // Left boundary
    public float maxX = 8.0f;            // Right boundary
    public float groundLevel = -4.0f;    // Ground level where the object will be destroyed

    private bool movingRight = true;     // Flag to control direction

    void Update()
    {
        MoveHorizontally();

        // Destroy the GameObject if it reaches the ground level
        if (transform.position.y <= groundLevel)
        {
            Destroy(gameObject);
        }
    }

    // Function to move the object left and right
    void MoveHorizontally()
    {
        // Check if the object is moving right
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // If it reaches the right boundary, switch direction and move down
            if (transform.position.x >= maxX)
            {
                movingRight = false;
                MoveDown();
            }
        }
        else
        {
            // Move left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // If it reaches the left boundary, switch direction and move down
            if (transform.position.x <= minX)
            {
                movingRight = true;
                MoveDown();
            }
        }
    }

    // Function to move the object down by dropAmount
    void MoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - dropAmount, transform.position.z);
    }
}
