using UnityEngine;
using System.Collections.Generic;

public class RandomVector3Generator
{
    // Fields for the min and max ranges for x, y, z
    private float xmin;
    private float xmax;
    private float ymin;
    private float ymax;
    private float zmin;
    private float zmax;

    // List to store generated Vector3 instances
    public List<Vector3> randomVectors { get; private set; }

    // Constructor to initialize the min and max values for x, y, and z
    public RandomVector3Generator(float xmin, float xmax, float ymin, float ymax, float zmin, float zmax)
    {
        // Set the range values
        this.xmin = xmin;
        this.xmax = xmax;
        this.ymin = ymin;
        this.ymax = ymax;
        this.zmin = zmin;
        this.zmax = zmax;

        // Initialize the list of vectors
        randomVectors = new List<Vector3>();
    }

    // Method to generate a specified number of random Vector3s within the range
    public void GenerateRandomVectors(int numberOfVectors)
    {
        // Clear the list to avoid duplicating entries if called multiple times
        randomVectors.Clear();

        for (int i = 0; i < numberOfVectors; i++)
        {
            // Generate random x, y, z values within the specified range
            float randomX = Random.Range(xmin, xmax);
            float randomY = Random.Range(ymin, ymax);
            float randomZ = Random.Range(zmin, zmax);

            // Create a new Vector3 and add it to the list
            Vector3 randomVector = new Vector3(randomX, randomY, randomZ);
            randomVectors.Add(randomVector);
        }
    }
}
