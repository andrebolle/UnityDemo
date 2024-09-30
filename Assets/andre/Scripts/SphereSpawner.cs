using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab; // Assign a sphere prefab in the Inspector

    void Start()
    {
        // Get the singleton instance and generate 5 random vectors
        PointsSingleton generator = PointsSingleton.Instance();
        generator.GenerateRandomVectors(2500);

        // Loop through each generated vector and spawn a sphere at that location
        foreach (var randomVector in generator.randomVectors)
        {
            Instantiate(spherePrefab, randomVector, Quaternion.identity);
        }
    }
}
