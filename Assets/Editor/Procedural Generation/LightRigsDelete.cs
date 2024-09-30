using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LightRigsDelete : MonoBehaviour
{
    // Prefix for identifying the cubes to delete
    private static string prefix = "LightObject";

    // Create a menu item in the Unity Editor toolbar
    [MenuItem("Tools/Delete LightObjects")]
    public static void DeleteLightObjects()
    {
        // Find all objects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // Loop through all objects and find ones that start with the cubePrefix
        int deletedCount = 0;
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith(prefix))
            {
                // Destroy the GameObject
                GameObject.DestroyImmediate(obj);
                deletedCount++;
            }
        }

        Debug.Log($"{deletedCount} LightObjects deleted.");
    }
}

