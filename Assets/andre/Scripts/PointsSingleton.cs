using UnityEngine;
using System.Collections.Generic;

public class PointsSingleton
{
    private float xmin = -5f;
    private float xmax = 5f;
    private float ymin = 0f;
    private float ymax = 5f;
    private float zmin = -5f;
    private float zmax = 5f;

    public List<Vector3> randomVectors { get; private set; }

    private static PointsSingleton instance = null;
    private static readonly object lockObj = new object();
    private bool initialized = false;

    private PointsSingleton()
    {
        randomVectors = new List<Vector3>();
    }

    public static PointsSingleton Instance()
    {
        lock (lockObj)
        {
            if (instance == null)
            {
                instance = new PointsSingleton();
            }
        }
        return instance;
    }

    public void GenerateRandomVectors(int numberOfVectors)
    {
        if (!initialized)
        {
            randomVectors.Clear();

            for (int i = 0; i < numberOfVectors; i++)
            {
                float randomX = Random.Range(xmin, xmax);
                float randomY = Random.Range(ymin, ymax);
                float randomZ = Random.Range(zmin, zmax);

                randomVectors.Add(new Vector3(randomX, randomY, randomZ));
            }

            initialized = true; // Mark as initialized
        }
    }
}
