using UnityEngine;
using UnityEditor;

public class RandomCubeCreator : EditorWindow
{
    // Prefix for naming the cubes
    private static string cubePrefix = "RandomCube_";

    // Create a menu item in the Unity Editor toolbar
    [MenuItem("Tools/Create 10 Random Cubes")]
    public static void CreateRandomCubes()
    {
        // Loop to create 10 random cube game objects
        for (int i = 0; i < 10; i++)
        {
            // Create a new Cube GameObject
            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Name the cube with a recognizable prefix and index
            newCube.name = cubePrefix + i;

            // Assign a random position to the Cube
            newCube.transform.position = GetRandomPosition();

            // Optionally, set a random scale for the Cube
            newCube.transform.localScale = GetRandomScale();
        }

        Debug.Log("10 Random Cubes created.");
    }

    // Helper function to generate a random position
    private static Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, y, z);
    }

    // Helper function to generate a random scale
    private static Vector3 GetRandomScale()
    {
        float scale = Random.Range(0.5f, 2f);
        return new Vector3(scale, scale, scale); // Uniform scale for simplicity
    }
}
