using UnityEngine;
using UnityEditor;

public class RandomCubeDeleter : EditorWindow
{
    // Prefix for identifying the cubes to delete
    private static string cubePrefix = "RandomCube_";

    // Create a menu item in the Unity Editor toolbar
    [MenuItem("Tools/Delete Random Cubes")]
    public static void DeleteRandomCubes()
    {
        // Find all objects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // Loop through all objects and find ones that start with the cubePrefix
        int deletedCount = 0;
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith(cubePrefix))
            {
                // Destroy the GameObject
                GameObject.DestroyImmediate(obj);
                deletedCount++;
            }
        }

        Debug.Log($"{deletedCount} Random Cubes deleted.");
    }
}
