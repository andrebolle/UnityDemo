using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [Header("Cube Settings")]
    [Tooltip("Assign cube GameObjects in the Inspector")]
    public List<GameObject> cubes = new List<GameObject>();
    private PointsSingleton generator;

    void Start()
    {
        // Initialize the singleton instance and generate random vectors
        generator = PointsSingleton.Instance();
        generator.GenerateRandomVectors(10); // Increase if needed

        // Start movement routines for each cube
        foreach (var cube in cubes)
        {
            StartCoroutine(MoveRoutine(cube));
        }
    }

    /// <summary>
    /// Coroutine that manages the movement sequence for a specific cube.
    /// </summary>
    IEnumerator MoveRoutine(GameObject cube)
    {
        float waitDuration;      // Time to wait before moving
        float moveDuration;    // Time to move between points (each way)

        while (true)
        {

            waitDuration = Random.Range(0.5f, 1f);
            yield return new WaitForSeconds(waitDuration);

            // Pick two new random points
            (Vector3 newStart, Vector3 newEnd) = PickNewRandomPoints();

            // Move from startPoint to endPoint
            moveDuration = Random.Range(0.5f, 2f);
            yield return StartCoroutine(MoveCube(cube ,newStart, newEnd, moveDuration));

            // Move back from endPoint to startPoint
            moveDuration = Random.Range(0.5f, 2f);
            yield return StartCoroutine(MoveCube(cube, newEnd, newStart, moveDuration));
        }
    }

    /// <summary>
    /// Picks two distinct random points from the generator and returns them.
    /// </summary>
    /// <returns>A tuple containing startPoint and endPoint.</returns>
    (Vector3, Vector3) PickNewRandomPoints()
    {
        if (generator.randomVectors.Count < 2)
        {
            Debug.LogWarning("Not enough random vectors to move the cube.");
            return (Vector3.zero, Vector3.zero); // Return default vectors if not enough points
        }

        int randomIndex1 = Random.Range(0, generator.randomVectors.Count);
        int randomIndex2;

        // Ensure the second random point is different from the first
        do
        {
            randomIndex2 = Random.Range(0, generator.randomVectors.Count);
        } while (randomIndex1 == randomIndex2);

        Vector3 newStart = generator.randomVectors[randomIndex1];
        Vector3 newEnd = generator.randomVectors[randomIndex2];

        return (newStart, newEnd);
    }

    /// <summary>
    /// Coroutine to move the cube from start to end over the specified duration.
    /// </summary>
    IEnumerator MoveCube(GameObject cube, Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0f;
        cube.transform.position = start;

        while (elapsed < duration)
        {
            // Interpolate the cube's position
            cube.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the cube reaches the exact end position
        cube.transform.position = end;
    }
}
